using KronosTech.Player;
using KronosTech.Room.ContentDisplay;
using KronosTech.ShowroomGeneration.Room.Videoplayer;
using System.Collections;
using UnityEngine;

public class RoomVideoplayerAutoStoper : MonoBehaviour
{
    [SerializeField] private ContentDisplayVideos m_videosDisplay;

    private Transform m_playerTF;
    private const float k_stopDistance = 6f;

    private Coroutine m_checkDistanceCoroutine;

    private void OnEnable()
    {
        m_videosDisplay.OnPlay += () => m_checkDistanceCoroutine = StartCoroutine(CheckDistance());
        m_videosDisplay.OnPause += StopSearch;
        m_videosDisplay.OnVideoChange += (index, name) => StopSearch();
    }
    private void OnDisable()
    {
        m_videosDisplay.OnPlay -= () => m_checkDistanceCoroutine = StartCoroutine(CheckDistance());
        m_videosDisplay.OnPause -= StopSearch;
        m_videosDisplay.OnVideoChange -= (index, name) => StopSearch();
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

        m_videosDisplay.ForcePause();
    }

    private void StopSearch()
    {
        if (m_checkDistanceCoroutine != null) StopCoroutine(m_checkDistanceCoroutine);
    }
}