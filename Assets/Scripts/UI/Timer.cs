using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time;

    public bool TimerActive = true;

    TMP_Text timerText;
    private void Start()
    {
        timerText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (TimerActive)
        {
            time += Time.deltaTime;
        }
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}",
            (int)(time / 60),
            (int)(time % 60),
            (int)((time * 1000) % 1000));

    }
}
