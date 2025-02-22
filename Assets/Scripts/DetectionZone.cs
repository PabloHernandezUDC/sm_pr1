using UnityEngine;
using UnityEngine.AI;

public class DetectionZone
    : MonoBehaviour
{
    Guard parent;
    NavMeshAgent parentAgent;

    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent.GetComponent<Guard>();
        parentAgent = parent.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Thief")
        {
            parent.patrolling = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Thief")
        {
            parent.chase_time = 0;
        }
    }
}
