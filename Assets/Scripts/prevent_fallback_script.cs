using UnityEngine;

public class prevent_fallback_Script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Ball"))
        {
            return;
        }

        Vector2 toBall = other.transform.position - transform.position;
        if(Vector2.Dot(toBall, transform.up) < 0f)
        {
            other.gameObject.layer = LayerMask.NameToLayer("Default");
            other.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }
}
