using System.Collections;
using UnityEngine;
using Google.Play.Review;

public class InAppReviewsManager : MonoBehaviour
{
    // Create instance of ReviewManager
    private ReviewManager _reviewManager;
    private PlayReviewInfo _playReviewInfo;

    [SerializeField] private int numberOfWinsToShow = 5;
    private string key = "InAppReviewsCounter";

    public static InAppReviewsManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ShowReviewWindow()// вызывается из LevelProgressController
    {
        int value = PlayerPrefs.GetInt(key, 1);

        if(value == numberOfWinsToShow)
        {
            PlayerPrefs.SetInt(key, 1);// сбрасываем
            StartCoroutine(RequestReviews());

            Debug.Log("SHOWING INAPP WINDOW");
        }
        else
        {
            value++;
            PlayerPrefs.SetInt(key, value);

            Debug.Log($"INAPP WINDOW COUNT = {value}");
        }
    }

    IEnumerator RequestReviews()
    {
        _reviewManager = new ReviewManager();

        // Request a ReviewInfo Object
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        _playReviewInfo = requestFlowOperation.GetResult();

        // Launch the InApp Review Flow
        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        // The flow has finished. The API does not indicate whether the user
        // reviewed or not, or even whether the review dialog was shown. Thus, no
        // matter the result, we continue our app flow.
    }
}
