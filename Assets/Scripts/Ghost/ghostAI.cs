using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ghostAI : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    public float runAwayDistance = 10f;  
    public float rangeForWandering = 10f;
    public float wanderTimer = 3f;

    private float timer;
    //private bool isFleeing = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < runAwayDistance)
        {
                FleeFromPlayer();
        }
        else
        {
                WanderAround();
        }
    }

    private void FleeFromPlayer()
    {
        if (agent.remainingDistance <= 1f || timer <= 0f)
        {

            Vector3 directionFromPlayer = transform.position - player.position;
            Vector3 runToPosition = transform.position + directionFromPlayer.normalized * runAwayDistance;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(runToPosition, out hit, runAwayDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
            else
            {
                print("Nije nadjena pozicija");
            }
            timer = wanderTimer;

        }
        else
        {
            timer -= Time.deltaTime;

        }

    }

    private void WanderAround()
    {
        if (agent.remainingDistance <= 1f || timer<=0f)
        {
            Vector3 randNavMeshPoint = PickRandNavMeshPos();
            agent.SetDestination(randNavMeshPoint);
            timer = wanderTimer;
        }
        else
        {
            timer -= Time.deltaTime;
                
        }

    }

    private Vector3 PickRandNavMeshPos()
    {
        Vector3 randomDirection = Random.insideUnitSphere * rangeForWandering;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, rangeForWandering, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }
}
