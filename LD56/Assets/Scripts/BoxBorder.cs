using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBorder : MonoBehaviour
{
    public Vector3 Direction;
    public bool ball;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        var a = other.GetComponent<CreatureController>();
        if (a!=null)
        {
            Debug.Log("Direction " + Direction);
            a.StartPushState(ball, Direction);
        }
    }

}
