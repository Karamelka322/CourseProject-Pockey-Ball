﻿using UnityEngine;

public class BallTracker : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, _ball.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * _speed);
    }
}