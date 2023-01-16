using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    private Vector2 speedVector = Vector2.zero;
    private float speedX = 10f;

    private GameObject movingObjects;
    private Rigidbody2D movingObjectsRb;

    private void Start()
    {
        movingObjects = GameObject.FindGameObjectWithTag("MovingObjects");
        movingObjectsRb = movingObjects.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        speedVector.x = speedX;
        speedVector.y = movingObjectsRb.velocity.y;

        movingObjectsRb.velocity = speedVector;
    }
}
