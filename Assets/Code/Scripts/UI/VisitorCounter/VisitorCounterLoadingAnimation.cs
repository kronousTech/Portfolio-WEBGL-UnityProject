using TMPro;
using UnityEngine;

public class VisitorCounterLoadingAnimation : MonoBehaviour
{
    private TextMeshPro _display;

    private const string FULL_TEXT = "Loading visitor count value...";
    private const float WAIT_TIME = 1.0f;
    private const int INDEX_TRESHOLD = 4;

    private int _index;
    private int Index
    {
        get => _index;
        set => _index = (value + INDEX_TRESHOLD) % INDEX_TRESHOLD;
    }
    private float _currentTime;

    private void Awake()
    {
        _display = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if(_currentTime > WAIT_TIME) 
        {
            Index -= 1;

            _currentTime = 0;

            _display.text = FULL_TEXT.Substring(0, FULL_TEXT.Length - Index);
        }
    }

}
