using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : Singleton<AnimatorManager>
{
    [SerializeField] Animator _animator;
    [SerializeField] List<AnimatorSetup> _animatorSetups;

    public enum AnimationType
    {
        IDLE,
        RUN,
        DEATH,
        VICTORY
    }

    public void Play(AnimationType type, float currentSpeedFactor = 1)
    {
        _animatorSetups.ForEach(setup => { if (setup.animationType == type) _animator.SetTrigger(setup.trigger); _animator.speed = setup.speed * currentSpeedFactor; } );
    }

}

[System.Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimationType animationType;
    public string trigger;
    public float speed = 1f;
}