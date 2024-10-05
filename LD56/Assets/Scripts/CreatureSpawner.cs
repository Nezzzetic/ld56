using System.Collections;
using System.Collections.Generic;
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

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayer))
            {
                // Spawn the creature at the hit point
                GameObject spawnedCreature = Instantiate(creaturePrefab, hit.point, Quaternion.identity);

                // Assign the cube to push to the creature's script
            }
        }
    }
}
