using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUINotifications : MonoBehaviour
{
    [SerializeField] private BuffsDebuffsNotifications buffsDebuffsNotifications;
    public BuffsDebuffsNotifications BuffsDebuffsNotifications { get => buffsDebuffsNotifications; }

    [SerializeField] private GameUINotifications gameUINotifications;
    public GameUINotifications GameUINotifications { get => gameUINotifications; }

    [SerializeField] private AttackBan attackBan;
    public AttackBan AttackBan { get => attackBan; }
}
