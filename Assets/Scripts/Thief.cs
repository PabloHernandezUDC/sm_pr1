using System;
using Unity.VisualScripting;
using UnityEngine;

public class Thief : MonoBehaviour
{
    Transform trans;

    public float movementSpeed = 16f;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            trans.position += new Vector3(movementSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            trans.position -= new Vector3(movementSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            trans.position += new Vector3(0f, 0f, movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            trans.position -= new Vector3(0f, 0f, movementSpeed * Time.deltaTime);
        }
    }
}
