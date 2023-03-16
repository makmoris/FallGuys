using UnityEngine;
using UnityEngine.Android;

public class PushNotification : MonoBehaviour
{
    [SerializeField]private bool isInit;

    private bool isMobile;

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

        isMobile = Application.isMobilePlatform;
    }

    private void OnEnable()
    {
        AnalyticsManager.InitializeCompletedEvent += Initialize;
    }
    private void OnDisable()
    {
        AnalyticsManager.InitializeCompletedEvent -= Initialize;
    }

    internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionDeniedAndDontAskAgain");
    }

    internal void PermissionCallbacks_PermissionGranted(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionCallbacks_PermissionGranted");
    }

    internal void PermissionCallbacks_PermissionDenied(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionCallbacks_PermissionDenied");
    }

    void Start()
    {
        if (isMobile)
        {
            var androidVersionInfo = new AndroidJavaClass("android.os.Build$VERSION");
            var androidAPILevel = androidVersionInfo.GetStatic<int>("SDK_INT");

            if (androidAPILevel >= 33)
            {
                Debug.Log($"[TEST] Android API Level = {androidAPILevel} ( >= 33). Request the notification permission");

                if (Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
                {
                    // The user authorized use of the microphone.
                }
                else
                {
                    bool useCallbacks = true;
                    if (!useCallbacks)
                    {
                        // We do not have permission to use the microphone.
                        // Ask for permission or proceed without the functionality enabled.
                        Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
                    }
                    else
                    {
                        var callbacks = new PermissionCallbacks();
                        callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
                        callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
                        callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;
                        Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS", callbacks);
                    }
                }
            }
            else
            {
                Debug.Log($"[TEST] Android API Level = {androidAPILevel} ( < 33). Don't request the notification permission");
            }
        }
    }

    private void Initialize()
    {
        if (!isInit)
        {
            Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
            Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;

            isInit = true;

            Debug.Log("[A] Firebase Push Notification Initialization Ñompleted");
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
