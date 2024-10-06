using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartForce : MonoBehaviour
{
    public Rigidbody Rigidbodyrb;
    public Vector3 Force;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbodyrb.AddForce(Force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
