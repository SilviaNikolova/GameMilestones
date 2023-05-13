using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class NavMeshPlayer : MonoBehaviour
{
    private NavMeshAgent agent;
    public float moveSpeed = 5f;
    public Transform cameraTarget; // the camera's target
    public Vector3 cameraOffset = new Vector3(0, 10, -10); // camera offset from target
    public GameObject finalPoint; // the final point
    public GameObject winCanvas; // the canvas to display when the player wins
    public TextMeshProUGUI winText; // the text to display when the player wins

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        finalPoint.AddComponent<Rigidbody>();
        finalPoint.GetComponent<Rigidbody>().isKinematic = true;
        winCanvas.SetActive(false); // hide the win canvas initially
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                Debug.Log("New destination: " + hit.point);
            }
        }

        // move the camera to follow the player
        Vector3 cameraPosition = transform.position + cameraOffset;
        Camera.main.transform.position = cameraPosition;
        Camera.main.transform.LookAt(transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == finalPoint)
        {
            Debug.Log("Won!");
            winCanvas.SetActive(true); // show the win canvas
            winText.text = "You reach the final point!"; // set the win text
        }
    }
}
