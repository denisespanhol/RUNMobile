using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : PowerUpBase
{
    [SerializeField] private float _speedBuff = 3f;

    public ParticleSystem particle;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        particle.Play();
        Player.Instance.SpeedUp(_speedBuff);
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        Player.Instance.SpeedNormal();
    }
}
