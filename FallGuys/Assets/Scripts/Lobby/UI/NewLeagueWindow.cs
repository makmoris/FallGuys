using UnityEngine;

public class NewLeagueWindow : MonoBehaviour
{
    [SerializeField] private RectTransform leagueIcons;

    private GameObject currentLeagueIcon;

    public void ShowLeagueIcon(int leagueIconIndex)
    {
        currentLeagueIcon = leagueIcons.GetChild(leagueIconIndex).gameObject;
        currentLeagueIcon.SetActive(true);
    }

    private void OnDisable()
    {
        if (currentLeagueIcon != null) currentLeagueIcon.SetActive(false);
    }
}
