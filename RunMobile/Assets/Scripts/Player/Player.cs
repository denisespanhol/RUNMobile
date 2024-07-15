using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public bool canRun;

    [Header("Player Settings")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private string tagToDanger = "Danger";

    [Header("Lerp Settings")]
    [SerializeField] private Transform _target;
    [SerializeField] private float lerpSpeed;

    private Vector3 _posToLerp;

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
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(tagToDanger))
        {
            canRun = false;
            GameManager.Instance.EndGame();
        }
    }

}
