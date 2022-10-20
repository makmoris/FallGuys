using System.Collections;
using UnityEngine;

public class PlayerEffector
{
    private readonly IPlayer _player;
    private readonly PlayerLimitsData _limitsData;

    private readonly UIIntermediary _intermediary;
    // IPowerUp powerUp;

    public PlayerEffector(IPlayer player, Bumper bumper, PlayerLimitsData limitsData, UIIntermediary intermediary)
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

                var resultHealth = _player.Health + bonus.Value;

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

                break;

            case BonusType.AddSpeed:

                break;

            case BonusType.AddDamage:

                break;
        }
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
