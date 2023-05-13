using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshPatrol : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    public Transform centerPoint;
    public float patrolRadius = 10f;
    public float patrolSpeed = 3f;
    public float chaseDistance = 5f; // The distance at which the agent starts chasing the player.
    public Transform player; // A reference to the player's transform.
    public int numWaypoints = 12;
    private Vector3[] waypoints;
    private int currentWaypointIndex = 0;
    private bool isChasing = false; // Whether the agent is currently chasing the player.
    
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = patrolSpeed;
        agent.obstacleAvoidanceType = UnityEngine.AI.ObstacleAvoidanceType.LowQualityObstacleAvoidance;
        GenerateWaypoints();
        agent.destination = waypoints[currentWaypointIndex];
    }

    void Update()
    {
        if (player == null) // Check if the player reference is null.
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < chaseDistance)
        {
            // The player is within chasing distance, switch to chase mode.
            agent.destination = player.position;
            isChasing = true;
        }
        else if (isChasing)
        {
            // The player is out of range, switch back to patrol mode.
            GenerateWaypoints();
            isChasing = false;
        }

        if (!isChasing && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            agent.destination = waypoints[currentWaypointIndex];
        }
    }

    void GenerateWaypoints()
    {
        List<Vector3> waypointList = new List<Vector3>();
        for (int i = 0; i < numWaypoints; i++)
        {
            float angle = i * (360f / numWaypoints);
            Vector3 waypointPos = centerPoint.position + Quaternion.Euler(0, angle, 0) * (Vector3.forward * patrolRadius);
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(waypointPos, out hit, 5f, UnityEngine.AI.NavMesh.AllAreas))
            {
                waypointList.Add(hit.position);
            }
        }
        waypoints = waypointList.ToArray();
    }
}
