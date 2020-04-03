using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : State
{

    public Transform target;
    NavMeshAgent agent;
    bool GOTOBEREND;
    private List<Transform> patrolroute;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CurrentState = cState.AFK;
    }

    // Update is called once per frame
    void Update()
    {
        if (GOTOBEREND && CurrentState == cState.Chase)
        {
            agent.SetDestination(target.position);
        }

        if(CurrentState == cState.Patrol)
        {

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            CurrentState = cState.Chase;
            GOTOBEREND = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            GOTOBEREND = false;
            agent.SetDestination(transform.position);
            CurrentState = cState.Patrol;
        }
    }

    void setPatrol()
    {
        patrolroute.Clear();
    }

}
