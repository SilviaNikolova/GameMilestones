using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NFS : MonoBehaviour {

    private CharacterState currentState;
    public NavMeshAgent agent;
    public Transform playerTransform;
    public float distanceThreshold = 5.0f;
    public Transform centerPoint;
    public float patrolRadius = 10f;
    public float patrolSpeed = 3f;
    public float chaseDistance = 5f;
    public int numWaypoints = 12;
    private Vector3[] waypoints;
    private int currentWaypointIndex = 0;
    private bool isChasing = false;

    void Start() {
        currentState = new IdleState();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        agent.obstacleAvoidanceType = UnityEngine.AI.ObstacleAvoidanceType.LowQualityObstacleAvoidance;
        GenerateWaypoints();
        agent.destination = waypoints[currentWaypointIndex];
    }

    void Update() {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer < chaseDistance)
        {
            // The player is within chasing distance, switch to chase mode.
            ChangeState(new WalkingState(playerTransform));
        }
        else if (!isChasing && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // The agent is in idle mode and has reached the current waypoint, move to the next waypoint.
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            agent.destination = waypoints[currentWaypointIndex];
        }

        // Execute current state behavior
        currentState.Execute();
    }

    private void ChangeState(CharacterState newState) {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
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

public abstract class CharacterState {
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}

public class IdleState : CharacterState {
    public override void Enter() {
        Debug.Log("Entering Idle State");
    }

    public override void Execute() {
        Debug.Log("Idle State");
    }

    public override void Exit() {
        Debug.Log("Exiting Idle State");
    }
}

public class WalkingState : CharacterState {

    private Transform targetTransform;
    private NavMeshAgent agent;

    public WalkingState(Transform targetTransform) {
        this.targetTransform = targetTransform;
    }

    public override void Enter() {
        Debug.Log("Entering Walking State");
        agent = GameObject.FindObjectOfType<NavMeshAgent>();
        agent.SetDestination(targetTransform.position);
    }

    public override void Execute() {
        Debug.Log("Walking State");
        if (agent.isActiveAndEnabled && agent.remainingDistance <= agent.stoppingDistance) {
            // Reached the target, transition back to idle state
            ChangeState(new IdleState());
        }
    }

    public override void Exit() {
        Debug.Log("Exiting Walking State");
    }

    private void ChangeState(CharacterState newState) {
        agent.ResetPath();
        CharacterState previousState = this;
        previousState.Exit();
        newState.Enter();
    }
}
