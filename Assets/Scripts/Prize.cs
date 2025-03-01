using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    EscapeDoor parent_door;
    Renderer rend;
    public bool picked_up;

    // Start is called before the first frame update
    void Start()
    {
        parent_door = GetComponentInParent<EscapeDoor>();
        rend = GetComponent<Renderer>();
    }
   
    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Thief" && picked_up == false)
        {
            picked_up = true;
            //Destroy(this.gameObject);
            rend.enabled = false;
            parent_door.prizes.Remove(this);
        }
    }

}
