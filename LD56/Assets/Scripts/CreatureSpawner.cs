using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public CreatureController creaturePrefab; // Drag the creature prefab here
    public GameObject creationFX; // Drag the creature prefab here
    public LayerMask floorLayer;

    void Update()
    {
        // Detect player click
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast from the mouse position to the gamefield
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits the floor layer
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayer))
            {
                // Only spawn creatures if the hit point is on the correct floor (Floor Type 1)
                if (hit.collider.CompareTag("Floor1"))
                {
                    CreatureController spawnedCreature = Instantiate(creaturePrefab, hit.point, Quaternion.identity,transform);
                    spawnedCreature.DefaultParent = transform;
                    //var fx = Instantiate(creationFX, spawnedCreature.transform.position, Quaternion.identity, transform);
                    //Destroy(fx, 1);
                }
                else if (hit.collider.CompareTag("Floor2"))
                {
                    // Do nothing, or provide feedback that the creature can't be spawned
                    Debug.Log("Can't create creatures on this type of floor!");
                }
            }
        }
    }
}
