using System.Collections;
using UnityEngine;
using DG.Tweening;

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
    [SerializeField] private float _jumpTimeOnAir;

    [Header("Animation Settings")]
    [SerializeField] private float _durationToScale = 1f;
    [SerializeField] private Ease _ease = Ease.OutBack;

    private BounceHelper _bounce;
    private Vector3 _posToLerp;
    private Vector3 _startPosition;
    private bool _isInvencible = false;
    private bool _canJump = false;
    private bool _isJumping = false;
    private float _currentSpeed;
    private float _posY;
    private float _targetY;
    private float _newY;
    private float _jumpStartTime;

    private void Start()
    {
        _bounce = GetComponent<BounceHelper>();
        canRun = true;
        _currentSpeed = speed;
        _startPosition = transform.position;
        _posY = transform.position.y;
        transform.DOScale(1, _durationToScale).SetEase(_ease);
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
        transform.Translate(_currentSpeed * Time.deltaTime * transform.forward);
    }

    public void HandleJump()
    {
        if (!_canJump) return;

        if (Input.GetMouseButtonDown(1) && !_isJumping)
        {
            _isJumping = true;
            _targetY = _posY + _jumpForce;
            _jumpStartTime = Time.time;

            AnimatorManager.Instance.Play(AnimatorManager.AnimationType.FLY);
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
        yield return new WaitForSeconds(_jumpTimeOnAir);

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
            AnimatorManager.Instance.Play(AnimatorManager.AnimationType.DEATH);
            GameManager.Instance.EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToWin))
        {
            canRun = false;
            AnimatorManager.Instance.Play(AnimatorManager.AnimationType.VICTORY);
            GameManager.Instance.EndGame();
        }
    }

    public void ToBounce()
    {
        _bounce.Bounce();
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

    public void SpeedUp(float speedBuff)
    {
        _currentSpeed += speedBuff;
    }

    public void SpeedNormal()
    {
        _currentSpeed = speed;
    }

    #endregion

}
