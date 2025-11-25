using KronosTech.Player;
using KronosTech.Room.ContentViewer;
using System.Collections;
using UnityEngine;

public class RoomVideoPlayerAutoStopper : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ContentDisplayVideos m_videosDisplay;

    private Transform m_playerTF;
    private const float k_stopDistance = 6f;

    private Coroutine m_checkDistanceCoroutine;

    private void OnEnable()
    {
        m_videosDisplay.OnPlay += StartDistanceSearchCallback;
        m_videosDisplay.OnPause += StopDistanceSearchCallback;
        m_videosDisplay.OnVideoChange += StopDistanceSearchCallback;
    }
    private void OnDisable()
    {
        m_videosDisplay.OnPlay -= StartDistanceSearchCallback;
        m_videosDisplay.OnPause -= StopDistanceSearchCallback;
        m_videosDisplay.OnVideoChange -= StopDistanceSearchCallback;
    }
    private void Awake()
    {
        m_playerTF = FindFirstObjectByType<FirstPersonControllerOLD>(FindObjectsInactive.Include).transform;
    }

    private IEnumerator CheckDistance()
    {
        var distance = Vector3.Distance(m_playerTF.position, m_videosDisplay.transform.position);

        while (distance < k_stopDistance)
        {
            yield return null;

            distance = Vector3.Distance(m_playerTF.position, m_videosDisplay.transform.position);
        }

        m_videosDisplay.Pause();
    }
    private void StartDistanceSearchCallback()
    {
        m_checkDistanceCoroutine = StartCoroutine(CheckDistance());
    }
    private void StopDistanceSearchCallback()
    {
        StopSearch();
    }
    private void StopDistanceSearchCallback(ContentDataUrl content)
    {
        StopSearch();
    }

    private void StopSearch()
    {
        if (m_checkDistanceCoroutine != null)
        {
            StopCoroutine(m_checkDistanceCoroutine);
        }
    }
}