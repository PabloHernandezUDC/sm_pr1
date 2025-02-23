using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject thief;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    bool canSeePlayer;
    LineRenderer line;

    Transform trans;
    NavMeshAgent agent;
    public List<Transform> patrol_points;

    int current_target;
    int n_of_targets;

    bool patrolling;
    float chase_time;
    public int max_chase_time;


    private void Start()
    {
        StartCoroutine(FOVRoutine());

        line = GetComponent<LineRenderer>();
        line.startWidth = 0.2f;
        line.endWidth = 0.8f;

        trans = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = patrol_points.First().position;

        current_target = 0;
        n_of_targets = patrol_points.Count;
        patrolling = true;
        chase_time = 0;

    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.016f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        line.enabled = false;
        // comprobamos si hay un jugador en nuestro radio
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            // cogemos su posición y la distancia hasta él
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // si está en nuestro "campo de vista" y la línea de visión no está obstruida, significa que podemos verlo
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                
                Ray ray = new Ray(transform.position, directionToTarget);
                RaycastHit hit;


                if (Physics.Raycast(ray, out hit, distanceToTarget, obstructionMask))
                {
                    // la vista está obstruida
                    canSeePlayer = false;
                }
                else
                {
                    // lo vemos sin obstrucciones
                    canSeePlayer = true;
                    patrolling = false;
                    chase_time = 0;

                    // y dibujamos la línea hasta el jugador
                    var points = new Vector3[2];
                    points[0] = transform.position;
                    points[1] = target.position;
                    line.enabled = true;
                    line.SetPositions(points);
                }
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer) // si lo hemos dejado de ver, cambiamos el valor
            canSeePlayer = false;
    }

    void Update()
    {
        if (patrolling)
        {
            // si hemos llegado, pasamos al siguiente
            if (trans.position.x == agent.destination.x && trans.position.z == agent.destination.z)
            {
                current_target++;
                agent.destination = patrol_points[current_target % n_of_targets].position;
            }
        }
        else
        {
            // si ha pasado demasiado tiempo, volvemos a patrullar
            if (chase_time >= max_chase_time)
            {
                patrolling = true;
                agent.destination = patrol_points[current_target % n_of_targets].position;
            }
            else // si no, actualizamos la posición y seguimos persiguiendo
            {
                agent.destination = thief.transform.position;
                chase_time += Time.deltaTime;
            }
        }
    }
}