using KronosTech.WebRequests;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace KronosTech.AssetBundles
{
    public class AssetBundleManifestLoader : MonoBehaviour
    {
        [Header("Debug View")]
        [SerializeField, ReadOnly] private List<string> m_availableBundles;

        private const string k_manifestPath =
            "https://raw.githubusercontent.com/kronousTech/Portfolio-WEBGL-AssetBundle-Storage/refs/heads/main/Portfolio-WEBGL-AssetBundle-Storage.manifest";

        public event Action<List<string>> OnAvailableBundlesLoad;

        private void Start()
        {
            WebRequest.Get(k_manifestPath, LoadAvailableBundlesListCallback);
        }

        private void LoadAvailableBundlesListCallback(WebRequestEventArgs args)
        {
            if (!args.IsSuccessful)
            {
                Debug.LogError($"{nameof(AssetBundleManifestLoader)}.cs: " +
                    $"Failed to load available bundles list. " +
                    $"Error Message: {args.Error}.");

                return;
            }
            var manifestText = args.Handler.text;
            var matches = Regex.Matches(manifestText, @"Name:\s*(.+)");

            m_availableBundles = matches.Cast<Match>()
                                        .Select(m => m.Groups[1].Value.Trim())
                                        .ToList();

            OnAvailableBundlesLoad?.Invoke(m_availableBundles);
        }
    }
}