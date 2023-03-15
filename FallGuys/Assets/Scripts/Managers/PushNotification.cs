using UnityEngine;

public class PushNotification : MonoBehaviour
{
    [SerializeField]private bool isInit;
    public TMPro.TextMeshProUGUI text;

    public static PushNotification Instance { get; private set; }
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

    private void OnEnable()
    {
        AnalyticsManager.InitializeCompletedEvent += Initialize;
    }
    private void OnDisable()
    {
        AnalyticsManager.InitializeCompletedEvent -= Initialize;
    }

    private void Initialize()
    {
        if (!isInit)
        {
            Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
            Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;

            isInit = true;

            Debug.Log("[A] Firebase Push Notification Initialization Ñompleted");

            text.text = "sfdsf";
        }
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);
    }
}
