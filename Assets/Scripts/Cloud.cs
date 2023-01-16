using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private ParticleSystem effects;

    private Rigidbody2D _pRigidbody;
    private Vector2 _velocity = new Vector2(0, 0);

    private void Start()
    {
        _pRigidbody = GetComponent<Rigidbody2D>();

        RandomizeVelocity();

        effects = GetComponent<ParticleSystem>();
    }

    private void RandomizeVelocity()
    {
        _velocity.x = Random.Range(-3, 0);

        _pRigidbody.velocity = _velocity;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            effects.Play();
        }
    }

}
