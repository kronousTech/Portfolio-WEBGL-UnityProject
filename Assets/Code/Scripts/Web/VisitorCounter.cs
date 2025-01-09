using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace KronosTech.Web
{
    public class VisitorCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _loadingText;
        [SerializeField] private TextMeshPro _visitorCountText; // Assign this in the Unity Editor

        private const string URL = "https://render-backend-ppap.onrender.com/counter"; // Replace with your server's URL if hosted online

        private void Awake()
        {
            _loadingText.enabled = true;
            _visitorCountText.enabled = false;
        }
        private void Start()
        {
            StartCoroutine(GetVisitorCounter());
        }

        // Coroutine to fetch the counter from the backend
        IEnumerator GetVisitorCounter()
        {
            using (UnityWebRequest request = UnityWebRequest.Get(URL))
            {
                // Send the request and wait for the response
                yield return request.SendWebRequest();

                _visitorCountText.enabled = true;
                _loadingText.gameObject.SetActive(false);

                // Check for errors
                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Error fetching visitor counter: " + request.error);
                    _visitorCountText.text = "Error fetching counter.";
                }
                else
                {
                    int counterValue = int.Parse(request.downloadHandler.text);

                    Debug.Log("Visitor Counter: " + counterValue);

                    _visitorCountText.text = "You are the " + counterValue + "th visitor";
                }
            }
        }
    }
}