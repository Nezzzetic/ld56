using System.Collections;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public float searchRadius = 5f; // The range to search for targets
    public float moveSpeed = 3f; // How fast the creature moves toward the target
    public float pushForce = 10f; // The force applied when pushing the target
    public float rotationSpeed = 5f; // Speed of rotation towards the target
    public LayerMask targetLayer; // Layer mask to filter the target search

    private Rigidbody targetCube; // The target cube or object
    private enum State { Searching, Moving, Pushing } // Define creature phases
    private State currentState = State.Searching; // Creature starts in the Searching state
    private bool isPushing = false; // Is the creature currently pushing?

    void Start()
    {
        // Start in the Searching state
        currentState = State.Searching;
    }

    void Update()
    {
        // Handle behavior based on the current state
        switch (currentState)
        {
            case State.Searching:
                SearchForTarget();
                break;
            case State.Moving:
                MoveTowardTarget();
                break;
            case State.Pushing:
                // Push phase is handled by a coroutine, so we don't need to do anything here
                break;
        }
    }

    // Searching phase: Continuously search for a valid target
    void SearchForTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius, targetLayer);

        // If we find a valid target, transition to the Moving phase
        if (hitColliders.Length > 0)
        {
            targetCube = hitColliders[0].GetComponent<Rigidbody>();
            if (targetCube != null)
            {
                // Stop searching and move towards the target
                currentState = State.Moving;
            }
        }
    }

    // Moving phase: Move towards the target cube
    void MoveTowardTarget()
    {
        if (targetCube != null)
        {
            // Move the creature toward the target cube
            var floorTarget = new Vector3(targetCube.transform.position.x, transform.position.y, targetCube.transform.position.z);
            Vector3 directionToTarget = (floorTarget - transform.position).normalized;
            transform.position += directionToTarget * moveSpeed * Time.deltaTime;

            // Smoothly rotate the creature to face the target
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Check if we've reached the target
            float distanceToTarget = Vector3.Distance(transform.position, floorTarget);
            if (distanceToTarget < 0.3f) // Adjust the distance threshold as needed
            {
                // Transition to the Pushing phase
                StartCoroutine(PushCube());
                transform.SetParent(targetCube.GetComponent<ObjectToFollow>().objectFollower.transform);
            }
        }
        else
        {
            // If the target is lost, return to the Searching state
            currentState = State.Searching;
        }
    }

    // Pushing phase: Push the cube for a certain duration
    IEnumerator PushCube()
    {
        // Switch to the Pushing state
        currentState = State.Pushing;
        isPushing = true;

        // Push for 2 seconds
        float pushDuration = 2f;
        while (pushDuration > 0)
        {
            if (isPushing && targetCube != null)
            {
                // Apply force to the cube in the direction the creature is facing
                Vector3 pushDirection = transform.forward; // Direction the creature is facing
                targetCube.AddForce(pushDirection * pushForce, ForceMode.Force);
            }
            pushDuration -= Time.deltaTime;
            yield return null;
        }

        // After pushing, the creature "falls asleep" or stops
        isPushing = false;

        // Optional: Destroy the creature after pushing
        transform.SetParent(transform.parent.parent);
        //Destroy(gameObject);
    }

    // Optional: Visualize the search radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
