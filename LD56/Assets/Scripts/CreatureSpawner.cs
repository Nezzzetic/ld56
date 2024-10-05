using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public GameObject creaturePrefab; // Drag the creature prefab here
    public Rigidbody cubeToPush; // Reference to the cube the creatures will push
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
                    GameObject spawnedCreature = Instantiate(creaturePrefab, hit.point, Quaternion.identity);

                    // Assign the cube to the creature's script so it can push it
                    CreatureController creatureController = spawnedCreature.GetComponent<CreatureController>();
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
