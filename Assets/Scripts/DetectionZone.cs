using UnityEngine;
using UnityEngine.AI;

public class DetectionZone
    : MonoBehaviour
{
    NavMeshAgent parentAgent;

    // Start is called before the first frame update
    void Start()
    {
        parentAgent = GetComponentInParent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Thief")
        {
            parentAgent.isStopped = true;
            other.gameObject.GetComponent<Thief>().movementSpeed = 0f; // creo que esto es una barbaridad
        }
    }

}
