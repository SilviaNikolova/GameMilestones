using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : MonoBehaviour
{
    public float lowHealthThreshold = 20.0f;
    public float safeDistanceThreshold = 10.0f;
    public float detectionRadius = 5.0f;
    public Transform playerTransform;

    private enum EnemyState {
        Idle,
        Patrolling,
        Chasing,
        Attacking,
        Fleeing
    }

    private EnemyState currentState = EnemyState.Idle;

    private bool playerInRange {
        get {
            return Vector3.Distance(transform.position, playerTransform.position) <= detectionRadius;
        }
    }

    private bool playerOutOfRange {
        get {
            return Vector3.Distance(transform.position, playerTransform.position) > detectionRadius;
        }
    }

    private bool canAttack {
        get {
            // Replace this with your own logic to determine if the enemy can attack the player
            return false;
        }
    }

    private bool lowHealth {
        get {
            return GetHealth() < lowHealthThreshold;
        }
    }

    private bool safeDistance {
        get {
            return Vector3.Distance(transform.position, playerTransform.position) > safeDistanceThreshold;
        }
    }

    private float GetHealth() {
        // Replace this with your own logic to get the enemy's health
        return 100.0f;
    }

    private void update() {
        switch (currentState) {
            case EnemyState.Idle:
                // Do idle actions
                if (playerInRange) {
                    currentState = EnemyState.Chasing;
                }
                break;
            case EnemyState.Chasing:
                // Do chasing actions
                if (playerOutOfRange) {
                    currentState = EnemyState.Idle;
                }
                else if (canAttack) {
                    currentState = EnemyState.Attacking;
                }
                break;
            case EnemyState.Attacking:
                // Do attacking actions
                if (playerOutOfRange) {
                    currentState = EnemyState.Chasing;
                }
                else if (lowHealth) {
                    currentState = EnemyState.Fleeing;
                }
                break;
            case EnemyState.Fleeing:
                // Do fleeing actions
                if (safeDistance) {
                    currentState = EnemyState.Idle;
                }
                break;
        }
    }
}
