using UnityEngine;

namespace KronosTech.AssetBundles
{
    public class AssetBundleLoadTester : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string m_assetBundleName;
        [SerializeField] private string m_assetName;

        //[Button("Load and Instantiate Bundle")]
        private void LoadAndInstantiateBundle()
        {
            AssetBundlesRequest.Load<GameObject>(m_assetBundleName, m_assetName, InstantiateBundleCallback);
        }

        private void InstantiateBundleCallback(AssetBundleLoadEventArgs<GameObject> args)
        {
            if(!args.IsSuccessful)
            {
                Debug.LogError($"{nameof(AssetBundleLoadTester)}.cs: " +
                    $"Failed to load bundle.\n" +
                    $"Message: {args.Error}");

                return;
            }

            Instantiate(args.Asset);
        }
    }
}