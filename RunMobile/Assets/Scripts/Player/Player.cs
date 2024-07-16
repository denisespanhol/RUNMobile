using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [HideInInspector] public bool canRun;

    [Header("Player Settings")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private string tagToDanger = "Danger";
    [SerializeField] private string tagToWin = "Endline";

    [Header("Lerp Settings")]
    [SerializeField] private Transform _target;
    [SerializeField] private float lerpSpeed;

    private Vector3 _posToLerp;
    private bool _isInvencible = false;

    private void Start()
    {
        canRun = true;
    }

    private void Update()
    {
        if (!canRun) return;

        Movement();
    }

    private void Movement()
    {
        _posToLerp = transform.position;
        _posToLerp.x = _target.position.x;

        transform.position = Vector3.Lerp(transform.position, _posToLerp, lerpSpeed * Time.deltaTime);
        transform.Translate(speed * Time.deltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(tagToDanger))
        {
            if (_isInvencible) return;
            canRun = false;
            GameManager.Instance.EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToWin))
        {
            canRun = false;
            GameManager.Instance.EndGame();
        }
    }

    #region POWER UPS

    public void ToStartInvencible(float duration)
    {
        _isInvencible = true;
        Invoke(nameof(ToEndInvencible), duration);
    }

    public void ToEndInvencible()
    {
        _isInvencible = false;
    }

    #endregion

}
