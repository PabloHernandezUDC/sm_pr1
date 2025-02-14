using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    Transform trans;
    NavMeshAgent agent;
    public List<Transform> targets;

    int current_target;
    int N_OF_TARGETS;


    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = targets.First().position;

        current_target = 0;
        N_OF_TARGETS = targets.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (trans.position.x == agent.destination.x && trans.position.z == agent.destination.z)
        {
            current_target++;
            agent.destination = targets[current_target  % N_OF_TARGETS].position;
        }            
    }
}
