using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    private event System.Action TimerFinishedEvent;

    public void StartTimer(float time, System.Action onTimerFinished)
    {
        TimerFinishedEvent = onTimerFinished;

        StartCoroutine(Timer(time));
    }

    IEnumerator Timer(float fillingTime)
    {
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / fillingTime;

            if (t > 1) t = 1;

            float time = Mathf.Lerp(fillingTime, 0.1f, t);
            int _time = Mathf.CeilToInt(time);
            timeText.text = string.Format("{0:D2}:{1:D2}", _time / 60, _time % 60);

            yield return null;
        }

        timeText.text = string.Format("{0:D2}:{1:D2}", 0, 0);

        yield return new WaitForSeconds(1f);

        TimerFinishedEvent?.Invoke();
    }
}
