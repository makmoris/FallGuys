using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameUIManager : MonoBehaviour
{
    [SerializeField] private BuffsDebuffsNotifications buffsDebuffsNotifications;

    [SerializeField] private AttackBan attackBan;

    public void SetCurrentPlayerGO(GameObject currentPlayerGO)
    {
        buffsDebuffsNotifications.SetCurrentPlayer(currentPlayerGO);
        attackBan.SetCurrentPlayer(currentPlayerGO);
    }

    public void ActivateAttackBan(GameObject gameObject, float banTime)
    {
        attackBan.ShowBanImage(gameObject, banTime);
    }
     public void DeactivateAttackBan(GameObject gameObj)
    {
        attackBan.HideBanImage(gameObj);
    }

    #region Notifications
    public void ShowLightningDebuffNotification(GameObject gameObject)
    {
        buffsDebuffsNotifications.ShowLightningDebuff(gameObject);
    }

    public void HideLightningDebuffNotification(GameObject gameObject)
    {
        buffsDebuffsNotifications.HideLightningDebuff(gameObject);
    }

    public void ShowShieldBuffNotification()
    {
        buffsDebuffsNotifications.ShowShieldBuff();
    }
    public void HideShieldBuffNotification()
    {
        buffsDebuffsNotifications.HideShieldBuff();
    }

    #endregion
}
