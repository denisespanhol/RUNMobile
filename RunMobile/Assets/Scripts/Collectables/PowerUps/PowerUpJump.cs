using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpJump : PowerUpBase
{
    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        Player.Instance.ToJump();
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        Player.Instance.ToNotJump();
    }
}
