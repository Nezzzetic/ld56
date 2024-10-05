using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToFollow : MonoBehaviour
{
    public ObjectFollower objectFollower;
    // Start is called before the first frame update
    void Start()
    {
        ObjectFollower obj = Instantiate(objectFollower);
        obj.target = gameObject;
        objectFollower=obj;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
