using UnityEngine;

public class mouth_script : MonoBehaviour
{
    public bool lit = true;
    public GameObject glow;

    private Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
        SetLit(lit);
    }

    public void SetLit(bool on)
    {
        lit = on;
        glow.SetActive(on);
        col.enabled = on;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            game_manager.instance.MouthHit(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            game_manager.instance.MouthHit(this);
        }
    }
}
