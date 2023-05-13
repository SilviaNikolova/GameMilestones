using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Week2PlayerScript : MonoBehaviour
{
Rigidbody2D rb;

public float maxSpeed;
public float jumpForce;

public float acceleration;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // gets the Rigidbody 2D component and assigns it to the variable rb
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // gets the horizontal axis value
        
        // if the horizontal axis value is greater than 0, then the player is moving right
        rb.AddForce(new Vector2 (acceleration * horizontal, 0));
        
        //if spacebar is pressed, then the player jumps
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
     
        }
    }
}