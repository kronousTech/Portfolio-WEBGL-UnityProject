using KronosTech.WebRequests;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace KronosTech.AssetBundles
{
    public static class AssetBundlesRequest
    {
        private const string k_bundlesFolderPath = "https://raw.githubusercontent.com/kronousTech/Portfolio-WEBGL-AssetBundle-Storage/main/";

        private static string SavedBundlesFolderPath
        {
            get => Application.persistentDataPath + "/AssetBundles/";
        }

        public static AssetBundle GetBundle(string assetBundleName)
        {
            return AssetBundle.LoadFromFile(GetSavedBundlePath(assetBundleName));
        }

        public static async void Load<T>(string assetBundleName, string assetName, Action<AssetBundleLoadEventArgs<T>> callback)
            where T : UnityEngine.Object
        {
            var bundlePath = GetSavedBundlePath(assetBundleName);

            if (File.Exists(bundlePath))
            {
                var bundle = AssetBundle.LoadFromFile(bundlePath);
                var asset = bundle.LoadAsset<T>(assetName);

                if(asset == null)
                {
                    var message = "Asset from bundle is null";

                    Debug.LogError($"{nameof(AssetBundlesRequest)}.cs: {message}");

                    callback?.Invoke(new(message));

                    return;
                }

                bundle.Unload(false);

                callback?.Invoke(new(asset as T));
            }
            else
            {
                Debug.LogWarning($"{nameof(AssetBundlesRequest)}.cs: " +
                    $"Bundle not found at directory {bundlePath}. \n" +
                    $"Trying to download...");

                Download(assetBundleName, (args) => TryToLoadAfterDownload(args, assetBundleName, assetName, callback));
            }
        }
        private static void TryToLoadAfterDownload<T>(AssetBundleDownloadEventArgs args, string assetBundleName, string assetName, Action<AssetBundleLoadEventArgs<T>> callback)
            where T : UnityEngine.Object
        {
            if(!args.IsSuccessful)
            {
                callback?.Invoke(new (args.Error));

                return;
            }

            Load(assetBundleName, assetName, callback);
        }

        public static void Download(string name, Action<AssetBundleDownloadEventArgs> callback)
        {
            var versionFilePath = GetSavedBundlesVersionPath(name);
            var bundleVersion = string.Empty;

            if (File.Exists(versionFilePath))
            {
                var sr = new StreamReader(versionFilePath);
                bundleVersion = GetVersionFromText(sr.ReadToEnd());
                sr.Close();

                Debug.LogWarning($"{nameof(AssetBundlesRequest)}.cs: " +
                    $"Version file available, version : {bundleVersion}.");
            }
            else
            {
                Directory.CreateDirectory(GetSavedBundleFolderPath(name));
            }

            WebRequest.Get(
                url: GetDownloadableBundleManifestPath(name),
                callback: (args) => CompareVersionsAndDownloadCallback(args, name, bundleVersion, callback));
        }
        private static void CompareVersionsAndDownloadCallback(WebRequestEventArgs args, string name, string currentVersion, Action<AssetBundleDownloadEventArgs> callback)
        {
            if(!args.IsSuccessful)
            {
                Debug.LogError($"{nameof(AssetBundlesRequest)}.cs: " +
                    $"Failed to download asset bundles manifest. " +
                    $"Message: {args.Error}.");

                callback?.Invoke(new(args.Error));
                
                return;
            }

            var regex = new Regex(@"BundleVersion:\s+(\d+)");
            var match = regex.Match(args.Handler.text);
            var version = match.Groups[1].Value;

            if(!version.Equals(currentVersion))
            {
                if (!string.IsNullOrEmpty(currentVersion))
                {
                    File.Delete(GetSavedBundlesVersionPath(name));
                    File.Delete(GetSavedBundlePath(name));

                    Debug.LogWarning($"{nameof(AssetBundlesRequest)}.cs: " +
                        $"Deleted old files.");
                }

                WebRequest.Get(
                    url: GetDownloadableBundlePath(name), 
                    callback: (args) => DownloadNewBundleCallback(args, name, version, callback));
            }
            else
            {
                Debug.LogWarning($"{nameof(AssetBundlesRequest)}.cs: " +
                    $"The bundle is already updated and in cache.");

                callback?.Invoke(new());
            } 
        }
        private static void DownloadNewBundleCallback(WebRequestEventArgs args, string name, string version, Action<AssetBundleDownloadEventArgs> callback)
        {
            if(!args.IsSuccessful)
            {
                Debug.LogError($"{nameof(AssetBundlesRequest)}.cs: " +
                    $"Failed to download the asset bundle. " +
                    $"Message: {args.Error}.");

                callback?.Invoke(new(args.Error));

                return;
            }

            Directory.CreateDirectory(GetSavedBundleFolderPath(name));

            File.WriteAllBytes(GetSavedBundlePath(name), args.Handler.data);

            CreateVersionFile(GetSavedBundlesVersionPath(name), version);

            Debug.Log($"{nameof(AssetBundlesRequest)}.cs: " +
                $"Successfully downloaded asset bundle.");

            callback?.Invoke(new());
        }

        private static string GetDownloadableBundlePath(string name)
        {
            return k_bundlesFolderPath + name;
        }
        private static string GetDownloadableBundleManifestPath(string name)
        {
            return GetDownloadableBundlePath(name) + ".manifest";
        }
        private static string GetSavedBundleFolderPath(string name)
        {
            return SavedBundlesFolderPath + Path.GetDirectoryName(name);
        }
        private static string GetSavedBundlePath(string name)
        {
            return GetSavedBundleFolderPath(name) + "/"+ Path.GetFileName(name);
        }
        private static string GetSavedBundlesVersionPath(string name)
        {
            return GetSavedBundlePath(name) + ".version";
        }
        private static string GetVersionFromText(string fileText)
        {
            return Regex.Replace(fileText, @"[\r\n\t\s]+", "");
        }
        private static void CreateVersionFile(string path, string version)
        {
            var sr = File.CreateText(path);
            sr.WriteLine(version);
            sr.Close();
        }
    }
}
