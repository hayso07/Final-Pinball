using UnityEngine;

public class reset_script : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            game_manager.instance.BallLost();
        }
    }
}
