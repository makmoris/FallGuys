using System.Collections;
using UnityEngine;

public class PlayerEffector
{
    private readonly IPlayer _player;
    private readonly PlayerLimitsData _limitsData;

    private readonly VisualIntermediary _intermediary;
    // IPowerUp powerUp;

    private bool isShieldActive;// ����� ��� �������, ����� �� ����� �������� ����
    private Coroutine shieldCoroutine = null;

    private bool isBot;

    public PlayerEffector(IPlayer player, Bumper bumper, PlayerLimitsData limitsData, VisualIntermediary intermediary)
    {
        _player = player;
        _limitsData = limitsData;
        bumper.OnBonusGot += ApplyBonus;

        _intermediary = intermediary;
        _intermediary.UpdateHealthInUI(_player.Health);

        if (bumper.GetComponent<CarDriverAI>() != null) isBot = true;
    }

    private void ActionTransition(float time)
    {
        CoroutineRunner.Run(Timer(time));
        // � ������� ����� ������� ��������� ��� ��� ���� ������
    }

    private void ApplyBonus(Bonus bonus)
    {
        switch (bonus.Type)
        {
            case BonusType.AddHealth:

                if (bonus.Value < 0 && isShieldActive && bonus.GetComponent<DeadZone>() == null)// ��������� - ����� �������� ���� � ����, ���� ����� �� �����
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
                        Bullet bullet = bonus.GetComponent<Bullet>();

                        if (bullet != null)
                        {
                            _intermediary.DestroyCar(bullet.GetParent());
                        }
                        else
                        {
                            _intermediary.DestroyCar();
                        }
                        //Debug.Log("Destroy in effector");
                    }
                }

                break;

            //case BonusType.AddSpeed:

            //    break;

            //case BonusType.AddDamage:

            //    break;

            case BonusType.AddShield:
                                                // ������ ������� � ���������� �� ������� ��������. 5 ��� � ������� 1/3 �� ������� ��������
                                                // ����� ����������, ����� ���������� �� ������� ��������, ������ ��������� 5 ������, ��������
                if (shieldCoroutine != null)
                {
                    CoroutineRunner.Stop(shieldCoroutine);
                    _intermediary.HideShield();
                }
                shieldCoroutine = CoroutineRunner.Run(ShieldActive(bonus.Value));

                break;

            case BonusType.AddGold:

                if(!isBot) LevelProgressController.Instance.AddGold((int)bonus.Value);

                break;
        }
    }

    IEnumerator ShieldActive(float time)
    {
        isShieldActive = true;
        _intermediary.ShowShield(time);
        Debug.Log("��� �����������");
        yield return new WaitForSeconds(time);
        isShieldActive = false;
        _intermediary.HideShield();
        Debug.Log("��� ��������");
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
