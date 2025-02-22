using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    Transform trans;
    NavMeshAgent agent;
    public List<Transform> targets;
    public Thief thief;

    int current_target;
    int n_of_targets;

    public bool patrolling;
    public float chase_time;
    public int max_chase_time;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = targets.First().position;

        current_target = 0;
        n_of_targets = targets.Count;
        patrolling = true;
        chase_time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolling)
        {
            if (trans.position.x == agent.destination.x && trans.position.z == agent.destination.z)
            {
                current_target++;
                agent.destination = targets[current_target  % n_of_targets].position;
            }            
        }
        else
        {
            agent.destination = thief.transform.position;
            chase_time += Time.deltaTime;
            if (chase_time >= max_chase_time)
            {
                patrolling = true;
            }
        }
    }
}
