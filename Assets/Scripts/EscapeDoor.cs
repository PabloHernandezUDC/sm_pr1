using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EscapeDoor : MonoBehaviour
{

    public List<Prize> prizes;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (prizes.Count == 0)
        {
            print("¡Enhorabuena! ¡Has robado todos los objetos!");
            Debug.Break();
        }
        else
        {
            print("Aún quedan objetos por robar...");
        }
    }

}
