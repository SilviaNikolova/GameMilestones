using UnityEngine;
using UnityEngine.UI;

public class BowlingBall : MonoBehaviour
{
    public Rigidbody ballObject;
    public float minSpeed = 0f;
    public float maxSpeed = 10f;
    public Button launchButton;

    private bool isReleased = false;

    void Start()
    {
        // Apply zero initial force to the ball to make it stable
        ballObject.AddForce(Vector3.zero, ForceMode.Impulse);

        // Add listener to the launch button
        launchButton.onClick.AddListener(LaunchBall);
        // ballObject.velocity = transform.forward * maxSpeed;

        // transform.rotation *= Quaternion.Euler(90f, 0f, 0f);

    }

    void Update()
    {
        if (!isReleased)
        {
            // Update the ball speed based on the slider value
            float speed = Mathf.Lerp(minSpeed, maxSpeed, 0.5f);
            ballObject.velocity = transform.forward * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lane"))
        {
            isReleased = false;
            ballObject.velocity = Vector3.zero;
        }
        else if (collision.gameObject.CompareTag("Pin"))
        {
            // Get the direction from the ball to the pin
            Vector3 direction = collision.transform.position - transform.position;
            direction.y = 0f; // Only consider the horizontal direction
            direction.Normalize();

            // Apply a force to the ball in the direction of the pin
            float force = Mathf.Lerp(minSpeed, maxSpeed, 0.5f);
            ballObject.AddForce(direction * force, ForceMode.Impulse);
        }
    }

    void LaunchBall()
    {
        // Calculate the direction towards the pins
        Vector3 direction = Vector3.zero;
        GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");
        if (pins.Length > 0)
        {
            Vector3 pinPosition = pins[0].transform.position;
            direction = pinPosition - transform.position;
            direction.y = 0f; // Only consider the horizontal direction
            direction.Normalize();
        }

        // Apply a force to the ball in the direction of the pins
        float force = Mathf.Lerp(minSpeed, maxSpeed, 0.5f);
        ballObject.AddForce(direction * force, ForceMode.Impulse);

        // Set isReleased to true so that Update() doesn't update the ball's speed anymore
        isReleased = true;
    }
        
    }



// using UnityEngine;
// using UnityEngine.UI;

// public class BowlingBall : MonoBehaviour
// {
//     public Slider slider;
//     public Rigidbody ballObject;
//     public float minSpeed = 0f;
//     public float maxSpeed = 10f;

//     private bool isReleased = false;

//     void Start()
//     {
//         // Apply zero initial force to the ball to make it stable
//         ballObject.AddForce(Vector3.zero, ForceMode.Impulse);
//     }

//   void Update()
// {
//     if (!isReleased)
//     {
//         // Update the ball speed based on the slider value
//         // float speed = Mathf.Lerp(minSpeed, maxSpeed, slider.value);
//         ballObject.velocity = transform.forward * maxSpeed;
//     }
// }

//     void OnCollisionEnter(Collision collision)
//     {
//         if (collision.gameObject.CompareTag("Lane"))
//         {
//             isReleased = false;
//             ballObject.velocity = Vector3.zero;
//         }
//         else if (collision.gameObject.CompareTag("Pin"))
//         {
//             // Get the direction from the ball to the pin
//             Vector3 direction = collision.transform.position - transform.position;
//             direction.y = 0f; // Only consider the horizontal direction
//             direction.Normalize();

//             // Apply a force to the ball in the direction of the pin
//             float force = Mathf.Lerp(minSpeed, maxSpeed, slider.value);
//             ballObject.AddForce(direction * force, ForceMode.Impulse);
//         }
//     }

//     void OnMouseDown()
//     {
//         isReleased = true;
//     }

// void OnMouseUp()
// {
//     // Calculate the direction towards the pins
//     Vector3 direction = Vector3.zero;
//     GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");
//     if (pins.Length > 0)
    
//     {
//         Vector3 pinPosition = pins[0].transform.position;
//         direction = pinPosition - transform.position;
//         direction.y = 0f; // Only consider horizontal direction
//         direction.Normalize();

//         // Apply a force to the ball in the direction of the pins
//         float force = Mathf.Lerp(minSpeed, maxSpeed, slider.value);
//         ballObject.AddForce(direction * force, ForceMode.Impulse);
//     }
// }
// }



// using UnityEngine;
// using UnityEngine.UI;

// public class BowlingBall : MonoBehaviour
// {
//     public Slider slider;
//     public Rigidbody ballObject;
//     public float minSpeed = 0f;
//     public float maxSpeed = 10f;

//     void Start()
//     {
//         // Apply zero initial force to the ball to make it stable
//         ballObject.AddForce(Vector3.zero, ForceMode.Impulse);
//     }

//     void Update()
//     {
//         // Update the ball speed based on the slider value
//         float speed = Mathf.Lerp(minSpeed, maxSpeed, slider.value);
//         ballObject.velocity = transform.forward * speed;
//     }
// }
