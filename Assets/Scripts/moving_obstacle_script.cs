using UnityEngine;

public class moving_obstacle_script : MonoBehaviour
{
    public float speed = 2.5f;
    public float distance = 3.5f;
    public bool reverse;
    public AudioClip hitSound;

    private Rigidbody2D rb;
    float startX;
    int direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startX = rb.position.x;

        if (reverse)
        {
            direction = -1;
        }
    }

    void FixedUpdate()
    {
        Vector2 pos = rb.position;
        pos.x += direction * speed * Time.fixedDeltaTime;

        if (pos.x >= startX + distance)
        {
            pos.x = startX + distance;
            direction = -1;
        }
        else if (pos.x <= startX - distance)
        {
            pos.x = startX - distance;
            direction = 1;
        }

        rb.MovePosition(pos);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            game_manager.instance.AddScore(500);
            game_manager.instance.audioSource.PlayOneShot(hitSound);
        }
    }
}
