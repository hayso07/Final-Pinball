using UnityEngine;

public class launcher_script : MonoBehaviour
{
    public float minSpeed = 15f;
    public float maxSpeed = 45f;
    public float chargingTime = 1f;

    Rigidbody2D ball;
    float charge;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ball"))
        {
            ball = other.attachedRigidbody;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            ball = null;
            charge = 0f;
        }
    }

    void Update()
    {
        if (ball == null)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            charge = Mathf.Min(charge + Time.deltaTime / chargingTime, 2f);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ball.linearVelocity = transform.up * Mathf.Lerp(minSpeed, maxSpeed, charge);
            charge = 0f;
        }
    }
}
