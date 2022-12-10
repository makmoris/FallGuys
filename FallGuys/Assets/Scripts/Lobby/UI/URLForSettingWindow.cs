using UnityEngine;

public class URLForSettingWindow : MonoBehaviour
{
    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("https://docs.google.com/document/d/16rhExHTAmGJTQdgmJzI8vgW0UsR9qvXN/edit#heading=h.gjdgxs");
    }

    public void OpenTheTermsOfService()
    {
        Application.OpenURL("https://docs.google.com/document/d/1biDk3Nv9UZ_k2kNqTfeW1v35SN3W8wj7/edit#heading=h.gjdgxs");
    }
}
