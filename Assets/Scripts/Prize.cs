using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    EscapeDoor parent_door;

    // Start is called before the first frame update
    void Start()
    {
        parent_door = GetComponentInParent<EscapeDoor>();
    }
   
    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Thief")
        {
            Destroy(this.gameObject);
            parent_door.prizes.Remove(this);
        }
    }

}
