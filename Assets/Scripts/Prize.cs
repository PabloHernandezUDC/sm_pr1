using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }
   
    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Thief")
        {
            Destroy(this.gameObject); // puede que esto tambi�n sea una barbaridad
        }
    }

}
