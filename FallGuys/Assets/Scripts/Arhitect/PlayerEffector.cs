using UnityEngine;

public class PlayerEffector
{
    private readonly IPlayer _player;
    private readonly PlayerLimitsData _limitsData;

    // IPowerUp powerUp;

    public PlayerEffector(IPlayer player, Bumper bumper, PlayerLimitsData limitsData)
    {
        _player = player;
        _limitsData = limitsData;
        bumper.OnBonusGot += ApplyBonus;
    }

    //private void ActionTransition(float time)
    //{
    //    CoroutineRunner.Run(Timer(time));
    //    // в течении этого времени действует тот или иной эффект
    //}

    private void ApplyBonus(Bonus bonus)
    {
        Debug.Log(bonus.Type + " PlayerEffector");
        // switch luchshe
        if (bonus.Type == BonusType.AddHealth)
        {
            var resultHealth = _player.Health + bonus.Value;
            if (resultHealth < _limitsData.MinHP)
                resultHealth = _limitsData.MinHP;
            else if (resultHealth > _limitsData.MaxHP)
                resultHealth = _limitsData.MaxHP;
        }
    }

    //private IEnumerator Timer(float time)
    //{
    //    // ...
    //}
}
