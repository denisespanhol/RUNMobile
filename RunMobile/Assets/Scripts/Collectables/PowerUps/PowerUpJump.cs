using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpJump : PowerUpBase
{
    public ParticleSystem particle;
    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        particle.Play();
        Player.Instance.ToJump();
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        Player.Instance.ToNotJump();
    }
}
