using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : State
{

    public Transform target;
    NavMeshAgent agent;
    bool GOTOBEREND;
    private List<Vector3> PatrolRoute = new List<Vector3>();
    private int pDist=10;
    int i = 0;
    private float AttackTimer = 2;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CurrentState = cState.AFK;
    }

    // Update is called once per frame
    void Update()
    {
        //Update Navmesh agent target
        if (GOTOBEREND && CurrentState == cState.Chase|| GOTOBEREND && CurrentState == cState.Attack)
        {
            agent.SetDestination(target.position);
        }
        //Attack Check if in range
        if (Vector3.Distance(transform.position,target.position)<=agent.stoppingDistance)
        {
            CurrentState = cState.Attack;
            if (AttackTimer > 0){ AttackTimer -= Time.deltaTime; } else { AttackTimer = 2; attack(); }
        } else if(CurrentState==cState.Attack&&Vector3.Distance(transform.position, target.position)>=agent.stoppingDistance) { CurrentState = cState.Chase;}

        //Patrol
        if(CurrentState == cState.Patrol)
        {
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                updatePatrol();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            CurrentState = cState.Chase;
            GOTOBEREND = true;
            PatrolRoute.Clear();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            GOTOBEREND = false;
            agent.SetDestination(transform.position);
            CurrentState = cState.Patrol;
            setPatrol();
            updatePatrol();
        }
    }

    void setPatrol()
    {
        PatrolRoute.Add(transform.forward * pDist);
        PatrolRoute.Add(-transform.forward * pDist);
        PatrolRoute.Add(transform.right * pDist);
        PatrolRoute.Add(-transform.right * pDist);
    }

    void updatePatrol()
    {
        if (i < 3)
        {
            i++;
            agent.SetDestination(PatrolRoute[i]);
           // Debug.Log(agent.SetDestination(PatrolRoute[i]));
        } else { i = 0; }
    }

    void attack()
    {
        Debug.Log("Attack");
    }
}
