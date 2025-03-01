using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{


    /*
    
    Comportamiento del agente guardia:

    IF      el ladr�n est� lo bastante cerca THEN lo captura
    ELSE IF ve al ladr�n                     THEN lo persigue
    ELSE IF ha visto al ladr�n               THEN patrulla cerca del premio
    ELSE                                          patrulla con normalidad
    
    */


    // Referencias a otros objetos y componentes
    public GameObject thief, guarded_prize;
    public LayerMask target_mask, obstruction_mask;
    Transform trans;
    NavMeshAgent agent;

    // Patrulla y persecuci�n
    public List<Transform> patrol_points;
    int current_target;
    public int max_chase_time; // en segundos
    float chase_time;
    bool patrolling;
    
    // Vista
    [Range(0, 360)]
    public float angle;
    public float capture_range;
    public float radius;
    bool alert_mode;
    bool can_see_player;

    // Gr�ficos
    LineRenderer line;
    Renderer rend;
    const float BLINK_INTERVAL = 0.5f;
    float blink_time;
    public Material mat1, mat2;


    private void Start()
    {
        StartCoroutine(FOVRoutine());

        trans = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = patrol_points.First().position;

        current_target = 0;
        patrolling = true;
        chase_time = 0;

        rend = GetComponent<Renderer>();
        line = GetComponent<LineRenderer>();
        line.startWidth = 3;
        line.endWidth = 12f;
        blink_time = 0;

    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new(0.016f);

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
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, target_mask);

        if (rangeChecks.Length != 0)
        {
            // cogemos su posici�n y la distancia hasta �l
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // si est� en nuestro "campo de vista" y la l�nea de visi�n no est� obstruida, significa que podemos verlo
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // si el ladr�n est� lo suficientemente cerca, lo capturamos y paramos la ejecuci�n
                if (distanceToTarget < capture_range)
                {
                    print("El ladr�n ha sido capturado.");
                    Debug.Break();
                }
                
                Ray ray = new(transform.position, directionToTarget);


                if (Physics.Raycast(ray, out RaycastHit hit, distanceToTarget, obstruction_mask))
                {
                    // la vista est� obstruida
                    can_see_player = false;
                }
                else
                {
                    // lo vemos sin obstrucciones
                    can_see_player = true;
                    patrolling = false;
                    chase_time = 0;

                    // y dibujamos la l�nea hasta el jugador
                    var points = new Vector3[2];
                    points[0] = transform.position;
                    points[1] = target.position;
                    line.enabled = true;
                    line.SetPositions(points);

                    // cambiamos a la segunda patrulla (entre el primer punto y el premio asignado)
                    if (alert_mode == false)
                    {
                        alert_mode = true;
                        agent.speed *= 1.2f;
                        patrol_points.RemoveRange(1, patrol_points.Count - 1);
                        patrol_points.Add(guarded_prize.transform);
                    }
                }
            }
            else
                can_see_player = false;
        }
        else if (can_see_player) // si lo hemos dejado de ver, cambiamos el valor
            can_see_player = false;
    }

    void Update()
    {
        if (patrolling)
        {
            // si hemos llegado a un destino, pasamos al siguiente
            if (trans.position.x == agent.destination.x && trans.position.z == agent.destination.z)
            {
                current_target++;
                agent.destination = patrol_points[current_target % patrol_points.Count].position;
            }
        }
        else
        {
            // si ha pasado demasiado tiempo, dejamos de perseguir y volvemos a patrullar
            if (chase_time >= max_chase_time)
            {
                patrolling = true;
                agent.destination = patrol_points[current_target % patrol_points.Count].position;
            }
            else // si no, actualizamos la posici�n y seguimos persiguiendo
            {
                agent.destination = thief.transform.position;
                chase_time += Time.deltaTime;
            }
        }

        // la l�gica para cambiar de color peri�dicamente cuando estamos en modo alerta
        if (alert_mode)
        {
            blink_time += Time.deltaTime;

            if (blink_time > BLINK_INTERVAL)
            {
                if (rend.material.color == mat1.color)
                {
                    rend.material = mat2;
                }
                else
                {
                    rend.material = mat1;
                }
                blink_time -= BLINK_INTERVAL;
            }
        }
    }
}