using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class agente : MonoBehaviour
{
    Transform trans;
    NavMeshAgent agent;
    public List<Transform> targets;

    int current_target;
    int N_OF_TARGETS;

    Boolean automatic_control = true;
    public float movement_speed = 32f;

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
        if (automatic_control)
        {
            if (trans.position.x == agent.destination.x && trans.position.z == agent.destination.z)
            {
                current_target++;
                agent.destination = targets[current_target  % N_OF_TARGETS].position;
            }
            
            if (Input.anyKey)
            {
                agent.updatePosition = false;
                automatic_control = false;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(movement_speed * Time.deltaTime, 0f, 0f);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= new Vector3(movement_speed * Time.deltaTime, 0f, 0f);
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0f, 0f, movement_speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= new Vector3(0f, 0f, movement_speed * Time.deltaTime);
            }

        }
    }
}
