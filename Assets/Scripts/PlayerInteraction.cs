using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 1.0f;
    public LayerMask interactableLayer;

    private PlayerController playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerController>();
    }


    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        Vector2 direction = playerMovement.GetLastMovementDirection();

        // Define the size of the overlap box
        Vector2 size = new Vector2(1.0f, 1.0f);

        // Calculate the position of the overlap box in front of the player
        Vector2 position = (Vector2)transform.position + direction * interactionRange;

        // Perform the overlap box check
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(position, size, 0f, interactableLayer);


        // Initialize variables to find the closest collider
        Collider2D closestCollider = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            // Calculate the distance from the player to the hit collider
            float distance = Vector2.Distance(position, hitCollider.transform.position);

            // Check if this collider is closer than the previously found closest collider
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCollider = hitCollider;
            }
        }

        // If a closest collider was found, interact with it
        if (closestCollider != null)
        {
            closestCollider.GetComponent<Interactable>().OnInteract();
        }
    }
}
