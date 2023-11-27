using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hunter : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movement;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 faceRight;
    private Vector3 faceLeft;
    private float directionX;

    public float moveSpeed = 16.0f;

    void Start()
    {
        faceRight = transform.localScale;
        faceLeft = transform.localScale;
        faceLeft.x = faceLeft.x * -1;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        directionX = Input.GetAxis("Horizontal");
        Vector2 direction = movement.action.ReadValue<Vector2>().normalized;
        Vector2 moveAmount = moveSpeed * Time.deltaTime * direction;

        if (directionX > 0)
        {
            transform.localScale = faceRight;
        }
        if (directionX < 0)
        {
            transform.localScale = faceLeft;
        }

        rb.position += moveAmount;
    }
}
