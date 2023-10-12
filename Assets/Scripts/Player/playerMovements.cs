using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovements : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    private Animator animator;
    private Renderer rend;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
            animator.SetFloat("speed", 0.5f);
        else
            animator.SetFloat("speed", 0.0f);
        if (horizontal > 0)
            animator.SetFloat("direction", 1.0f);
        else if (horizontal < 0)
            animator.SetFloat("direction", -1.0f);
        rend.sortingOrder = (int)((transform.position.y - 2) * -10);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }
}
