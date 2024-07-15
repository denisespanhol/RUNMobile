using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float speed = 1f;

    [Header("Lerp Settings")]
    [SerializeField] private Transform _target;
    [SerializeField] private float lerpSpeed;


    private bool _canRun;
    private Vector3 _posToLerp;

    private void Start()
    {
        _canRun = true;
    }

    private void Update()
    {
        if (!_canRun) return;

        Movement();
    }

    private void Movement()
    {
        _posToLerp = transform.position;
        _posToLerp.x = _target.position.x;

        transform.position = Vector3.Lerp(transform.position, _posToLerp, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }
    
}
