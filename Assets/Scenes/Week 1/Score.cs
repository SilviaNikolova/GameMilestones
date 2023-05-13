using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public int score = 0;

    private void Start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected" + collision.gameObject.name);
            // score++;
            // Debug.Log("Score: " + score);
    }

}
