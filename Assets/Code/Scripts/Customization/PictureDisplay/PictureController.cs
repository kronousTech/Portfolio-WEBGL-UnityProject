using KronosTech.AssetManagement;
using KronosTech.Services;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace KronosTech.Customization.Pictures
{
    public static class PictureController
    {
        private static readonly System.Random s_Random = new();
        private static string m_manifestURL = "https://raw.githubusercontent.com/kronousTech/Portfolio-WEBGL-PC/main/Content/Bundles/gallery/memes.manifest";
        private static readonly List<Sprite> m_picturesList = new();
        private static int Index = 0;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            AssetsLoader.OnBundlesDownload += GetPictures;
        }

        private static void GetPictures()
        {
            var temporaryObject = new GameObject("TemporaryCoroutineRunner");
            var temporaryRunner = temporaryObject.AddComponent<CoroutineRunner>();

            temporaryRunner.StartCoroutine(CallWebRequest.GetRequest(m_manifestURL, (string data, string error) =>
            {
                if (!string.IsNullOrEmpty(error))
                {
                    Debug.LogError("Error getting bundles data: " + error);
                    return;
                }

                foreach (Match match in new Regex(@"- Assets/.*").Matches(data))
                {
                    var asset = new Asset(match.Value.Split('/')[^1], "memes", AssetCategory.gallery);

                    m_picturesList.Add(ServiceLocator.Instance.GetWebImagesService().LoadImage(asset));
                }

                m_picturesList.Shuffle();
            }));
        }

        public static void Shuffle<T>(this IList<T> parent)
        {
            int n = parent.Count;
            int k;

            while (n > 1)
            {
                n--;
                k = s_Random.Next(n + 1);
                (parent[k], parent[n]) = (parent[n], parent[k]);
            }
        }

        public static Sprite RequestPicture()
        {
            if(m_picturesList.Count == 0)
            {
                return null;
            }

            if(Index >= m_picturesList.Count)
            {
                Index = 0;

                m_picturesList.Shuffle();
            }

            return m_picturesList[Index++];
        }
    }
}