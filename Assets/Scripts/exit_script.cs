
using UnityEngine;

public class exit_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 30f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ball"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("Default");
            other.GetComponent<SpriteRenderer>().sortingOrder = 1;
            other.attachedRigidbody.linearVelocity = transform.up * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
