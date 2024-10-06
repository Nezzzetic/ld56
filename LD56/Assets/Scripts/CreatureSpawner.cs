using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public CreatureController creaturePrefab; // Drag the creature prefab here
    public GameObject creationFX;
    public GameObject NoFX;// Drag the creature prefab here
    public LayerMask floorLayer;
    public AudioSource[] CreateSounds;

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
                    CreatureController spawnedCreature = Instantiate(creaturePrefab, hit.point, Quaternion.LookRotation(Vector3.back), transform);
                    spawnedCreature.DefaultParent = transform;
                    var fx = Instantiate(creationFX, spawnedCreature.transform.position, Quaternion.identity, transform);
                    Destroy(fx, 1);
                    var i=Random.Range(0, CreateSounds.Length-1);
                    CreateSounds[i].Play();
                }
                else if (hit.collider.CompareTag("Floor2")|| hit.collider.CompareTag("lose"))
                {
                    // Do nothing, or provide feedback that the creature can't be spawned
                    var fx = Instantiate(NoFX, hit.point, Quaternion.identity, transform);
                    Destroy(fx, 1);
                }
            }
        }
    }
}
