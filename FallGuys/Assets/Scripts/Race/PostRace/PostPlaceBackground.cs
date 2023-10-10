using System.Collections.Generic;
using UnityEngine;

public class PostPlaceBackground : MonoBehaviour
{
    public enum BackgroundChangeVariants
    {
        random,
        queue
    }

    [SerializeField] private BackgroundChangeVariants backgroundChangeVariant;
    [Space]
    [SerializeField] private List<GameObject> backgrounds;

    private const string key = "PostPlaceBackgroundVariantIndex";

    private void OnEnable()
    {
        ChangeBackground();
    }

    private void ChangeBackground()
    {
        HideAll();

        switch (backgroundChangeVariant)
        {
            case BackgroundChangeVariants.random:

                int r = Random.Range(0, backgrounds.Count);
                ShowBackground(backgrounds[r]);

                break;
            case BackgroundChangeVariants.queue:

                int i = PlayerPrefs.GetInt(key, 0);
                ShowBackground(backgrounds[i]);

                i++;
                if (i == backgrounds.Count) i = 0;

                PlayerPrefs.SetInt(key, i);

                break;
        }
    }

    private void ShowBackground(GameObject bacgroundGO)
    {
        bacgroundGO.SetActive(true);
    }

    private void HideAll()
    {
        foreach (var back in backgrounds)
        {
            back.SetActive(false);
        }
    }
}
