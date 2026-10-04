using UnityEngine;

public class ring_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int hitsToDestroy = 3;
    public float bounce = 0.3f;
    public float bounceTime = 0.3f;

    int hits;
    float timer;
    Vector3 startScale;

    Rigidbody2D rb;

    void Start()
    {
        startScale = transform.localScale;
        timer = bounceTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            hits++;
            timer = 0f;
            game_manager.instance.AddScore(1000);
            game_manager.instance.audioSource.PlayOneShot(game_manager.instance.metalSound);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(timer < bounceTime)
        {
            timer += Time.deltaTime;
            float p = Mathf.Min(timer / bounceTime, 1f);
            float s = 1f + bounce * Mathf.Sin(p * Mathf.PI * 3f) * (1f - p);
            transform.localScale = startScale * s;
            if(p >= 1f && hits >= hitsToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }

}
