using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] private float velocity;
    
    
    private Vector2 _pastPosition;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            FollowMouse(Input.mousePosition.x - _pastPosition.x);
        }

        _pastPosition = Input.mousePosition;
    }

    private void FollowMouse(float speed)
    {
        transform.position += speed * Time.deltaTime * velocity * Vector3.right;
    }
}
