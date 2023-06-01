using System.Collections;
using UnityEngine;
using TMPro;

public class TimerBeforeStart : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private Color numberColor;
    [SerializeField] private Color wordColor;

    public event System.Action TimerBeforeStartFinishedEvent;

    public void StartTimer(float time)
    {
        timeText.color = numberColor;
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
            timeText.text = $"{Mathf.CeilToInt(time)}";

            yield return null;
        }

        timeText.color = wordColor;
        timeText.text = "GO!";

        yield return new WaitForSeconds(1f);

        TimerBeforeStartFinishedEvent?.Invoke();
    }
}
