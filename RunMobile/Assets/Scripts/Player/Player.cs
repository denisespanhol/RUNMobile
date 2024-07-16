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

    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpVelocity;

    private Vector3 _posToLerp;
    private Vector3 _startPosition;
    private bool _isInvencible = false;
    private bool _canJump = false;
    private bool _isJumping = false;
    private float _posY;
    private float _targetY;
    private float _newY;
    private float _jumpStartTime;

    private void Start()
    {
        canRun = true;
        _startPosition = transform.position;
        _posY = transform.position.y;
    }

    private void Update()
    {
        if (!canRun) return;

        Movement();
        HandleJump();
    }

    private void Movement()
    {
        _posToLerp = transform.position;
        _posToLerp.x = _target.position.x;

        transform.position = Vector3.Lerp(transform.position, _posToLerp, lerpSpeed * Time.deltaTime);
        transform.Translate(speed * Time.deltaTime * transform.forward);
    }

    public void HandleJump()
    {
        if (!_canJump) return;

        if (Input.GetMouseButtonDown(1) && !_isJumping)
        {
            _isJumping = true;
            _targetY = _posY + _jumpForce;
            _jumpStartTime = Time.time; // Tempo de início do salto

            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        // Subida
        while (transform.position.y < _targetY)
        {
            float t = (Time.time - _jumpStartTime) * _jumpVelocity;
            float newY = Mathf.Lerp(_posY, _targetY, t);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }

        // Pausa no topo do salto
        yield return new WaitForSeconds(0.1f);

        // Descida
        _jumpStartTime = Time.time; // Tempo de início da descida
        while (transform.position.y > _posY)
        {
            float t = (Time.time - _jumpStartTime) * _jumpVelocity;
            float newY = Mathf.Lerp(_targetY, _posY, t);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, _posY, transform.position.z);
        _isJumping = false;
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

    public void ToJump()
    {
        _canJump = true;
    }

    public void ToNotJump()
    {
        _canJump = false;
    }

    

    #endregion

}
