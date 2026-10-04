using System;
using UnityEngine;

public class ball_script : MonoBehaviour
{
    private Rigidbody2D rb;
    Boolean dropped = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

 
    void FixedUpdate()
    {
     
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.CompareTag("Wall"))
        {
            Vector2 contactPosition = other.GetContact(0).normal;
            Vector2 reflectedVelocity = Vector2.Reflect(rb.linearVelocity, contactPosition);
            rb.linearVelocity = reflectedVelocity.normalized * 10f;
        }
        else if(other.gameObject.CompareTag("Bumper"))
        {
            Vector2 contactPosition = other.GetContact(0).normal;
            float launchForce = 20f;
            rb.linearVelocity = contactPosition * launchForce;
        }
    }

    
}
