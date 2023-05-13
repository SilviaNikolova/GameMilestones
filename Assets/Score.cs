using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // create a colider detection log what is been colidered with
    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("colidered with " + collision.gameObject.name);
    if (collision.gameObject.name == "Cube (3)")
    {
        score++;
        Debug.Log("Collided with Cube 3");
        Debug.Log("Score: " + score);
    }
    else if (collision.gameObject.name == "Cube (4)")
    {
        score++;
        Debug.Log("Collided with Cube 4");
        Debug.Log("Score: " + score);
    }
    else if (collision.gameObject.name == "Cube (5)")
    {
        score++;
        Debug.Log("Collided with Cube 5");
        Debug.Log("Score: " + score);
    }
    }
}
