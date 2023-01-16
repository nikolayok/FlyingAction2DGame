using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovement : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D pRigidbody;

    private ParticleSystem particles;
    private TrailRenderer trail;

    private Vector2 liftVector = Vector2.zero;
    private Vector2 speedVector = Vector2.zero;

    private const float speedX = 3f;

    private const float liftForce = 5;

    private bool isPointerDown = false;

    private bool isMoving = false;

    private bool isLiftAllowed = true;

    //private int _maxSpeedY = 100;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pRigidbody = player.GetComponent<Rigidbody2D>();

        particles = player.GetComponent<ParticleSystem>();
        trail = player.GetComponent<TrailRenderer>();
    }

    private void FixedUpdate()
    {
        if ( ! isMoving)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Space) || isPointerDown)
        {
            Lift();
        }

        Move();
    }

    private bool IsLiftAllowed()
    {
        return isLiftAllowed;
    }

    private void AllowLift()
    {
        isLiftAllowed = true;

    }

    private void DisallowLift()
    {
        isLiftAllowed = false;
    }

    private void Lift()
    {
        if (!IsLiftAllowed())
            return;

        //liftVector = Vector2.up * liftForce * Time.fixedDeltaTime;

        liftVector = Vector2.up * liftForce;
        pRigidbody.AddForce(liftVector, ForceMode2D.Impulse);

        if (pRigidbody.velocity.y < 0)
            pRigidbody.velocity = Vector2.zero;

        else
            pRigidbody.velocity = liftVector;

        ////Debug.Log(liftVector);

        DisallowLift();
        Invoke("AllowLift", 0.5f);
    }

    public void ResetMovement()
    {
        PointerUp();
        ResetSpeed();
        StopMovement();
        DisablePlayerGravity();
        ResetPosition();
        ResetEffects();
    }

    private void ResetEffects()
    {
        particles.Stop();
        trail.emitting = false;

        particles.Clear();
        trail.Clear();
    }

    private void DisablePlayerGravity()
    {
        pRigidbody.gravityScale = 0;
    }

    private void StopMovement()
    {
        isMoving = false;
    }

    private void ResetSpeed()
    {
        liftVector = Vector2.zero;
        speedVector = Vector2.zero;

        pRigidbody.velocity = Vector2.zero;
        pRigidbody.angularVelocity = 0;
    }

    private void ResetPosition()
    {
        pRigidbody.position = Vector2.zero;
        pRigidbody.rotation = 0;
    }

    public void StartMovement()
    {
        isMoving = true;
        pRigidbody.gravityScale = 1;

        StartEffects();
    }

    private void StartEffects()
    {
        particles.Play();
        trail.emitting = true; ;
    }

    public void PointerDown()
    {
        isPointerDown = true;
    }

    public void PointerUp()
    {
        isPointerDown = false;
    }

    private void Move()
    {
        speedVector.x = speedX;
        speedVector.y = pRigidbody.velocity.y;

        pRigidbody.velocity = speedVector;
    }
}
