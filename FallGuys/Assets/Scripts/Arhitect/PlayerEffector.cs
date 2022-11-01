using System.Collections;
using UnityEngine;

public class PlayerEffector
{
    private readonly IPlayer _player;
    private readonly PlayerLimitsData _limitsData;

    private readonly VisualIntermediary _intermediary;
    // IPowerUp powerUp;

    private bool isShieldActive;// когда щит активен, игрок не может получать урон
    private Coroutine shieldCoroutine = null;

    public PlayerEffector(IPlayer player, Bumper bumper, PlayerLimitsData limitsData, VisualIntermediary intermediary)
    {
        _player = player;
        _limitsData = limitsData;
        bumper.OnBonusGot += ApplyBonus;

        _intermediary = intermediary;
        _intermediary.UpdateHealthInUI(_player.Health);
    }

    private void ActionTransition(float time)
    {
        CoroutineRunner.Run(Timer(time));
        // в течении этого времени действует тот или иной эффект
    }

    private void ApplyBonus(Bonus bonus)
    {
        switch (bonus.Type)
        {
            case BonusType.AddHealth:

                if (bonus.Value < 0 && isShieldActive)
                {

                }
                else
                {
                    var resultHealth = _player.Health + bonus.Value;
                    Debug.Log(resultHealth + " = " + _player.Health + " + " + bonus.Value);
                    if (resultHealth > _limitsData.MaxHP)
                    {
                        resultHealth = _limitsData.MaxHP;
                    }
                    else if (resultHealth <= 0)
                    {
                        resultHealth = 0;
                    }

                    _player.SetHealth(resultHealth);
                    _intermediary.UpdateHealthInUI(_player.Health);

                    if (_player.Health == 0)
                    {
                        _intermediary.DestroyCar();
                        Debug.Log("Destroy in effector");
                    }
                }

                break;

            //case BonusType.AddSpeed:

            //    break;

            //case BonusType.AddDamage:

            //    break;

            case BonusType.AddShield:
                                                // сейчас мерцает в зависмости от времени действия. 5 раз в течении 1/3 от времени действия
                                                // можно переделать, чтобы независимо от времени действия, всегда последние 5 секунд, например
                if (shieldCoroutine != null)
                {
                    CoroutineRunner.Stop(shieldCoroutine);
                    _intermediary.HideShield();
                }
                shieldCoroutine = CoroutineRunner.Run(ShieldActive(bonus.Value));

                break;
        }
    }

    IEnumerator ShieldActive(float time)
    {
        isShieldActive = true;
        _intermediary.ShowShield(time);
        Debug.Log("Щит активирован");
        yield return new WaitForSeconds(time);
        isShieldActive = false;
        _intermediary.HideShield();
        Debug.Log("Щит отключен");
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
