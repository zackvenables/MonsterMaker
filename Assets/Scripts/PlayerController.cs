using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D player;
    private Vector2 moveInput;
    private Vector2 lastMovementDirection;

    public float interactionRange = 1.0f;
    public LayerMask interactableLayer;

    [SerializeField] PlayerInteraction playerInteraction;

    Animator animator;

    void Awake()
    {
        speed = 8;
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public void HandleUpdate()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);

            lastMovementDirection = moveInput.normalized;

            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        player.velocity = moveInput * speed;
        

        playerInteraction.HandleUpdate();
    }

    public Vector2 GetLastMovementDirection()
    {
        return lastMovementDirection;
    }
}
