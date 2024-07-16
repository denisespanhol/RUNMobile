using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvencible : PowerUpBase
{
    [SerializeField] private float _duration = 2f;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        Player.Instance.ToStartInvencible(_duration);
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        Player.Instance.ToEndInvencible();
    }
}
