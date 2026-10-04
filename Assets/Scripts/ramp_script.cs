using TMPro;
using UnityEngine;

public class ramp_script : MonoBehaviour
{
    public string setLayer = "Ramp";
    public int sortingOrder = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            other.gameObject.layer = LayerMask.NameToLayer(setLayer);
            other.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
            game_manager.instance.AddScore(15000);
            game_manager.instance.audioSource.PlayOneShot(game_manager.instance.rampSound);
        }
    }
}
