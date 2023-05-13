using UnityEngine;

public class MovingSphere : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float changeDirectionTime = 2f;

    private float nextDirectionTime;
    private Vector3 direction;

    private void Start()
    {
        // Set the initial direction to a random vector
        direction = Random.onUnitSphere;
        nextDirectionTime = Time.time + changeDirectionTime;
    }

    private void Update()
    {
        // Move the sphere in its current direction
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Check if it's time to change direction
        if (Time.time >= nextDirectionTime)
        {
            // Set the next direction to a random vector
            direction = Random.onUnitSphere;
            nextDirectionTime = Time.time + changeDirectionTime;
        }
    }
}
