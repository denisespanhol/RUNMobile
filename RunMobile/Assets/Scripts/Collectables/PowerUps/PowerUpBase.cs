using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : CollectableBase
{
    [SerializeField] private float pwupDuration = 2f;

    protected override void OnCollect()
    {
        base.OnCollect();
        Player.Instance.ToBounce();
        StartPowerUp();
    }

    protected virtual void StartPowerUp()
    {
        Debug.Log("PowerUp Starts");
        Invoke(nameof(EndPowerUp), pwupDuration);
    }

    protected virtual void EndPowerUp()
    {
        Debug.Log("PowerUp Ends");
    }
}
