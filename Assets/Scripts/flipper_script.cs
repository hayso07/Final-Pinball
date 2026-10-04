using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public bool reverse;
    public AudioClip flipSound;

    void Start()
    {

    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.A) && !reverse) || (Input.GetKeyDown(KeyCode.D) && reverse))
        {
            game_manager.instance.audioSource.PlayOneShot(flipSound);
        }
    }

   
    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.A) && !reverse)
        {
            rb2d.AddTorque(1000f, ForceMode2D.Force);
        }
        else if (!reverse)
        {
            rb2d.AddTorque(-800f, ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.D) && reverse)
        {
            rb2d.AddTorque(-1000f, ForceMode2D.Force);
        }
        else if (reverse)
        {
            rb2d.AddTorque(800f, ForceMode2D.Force);
        }
        
    }

   

}
   