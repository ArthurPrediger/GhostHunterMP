using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ghost : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 16.0f;

    [SerializeField]
    private InputActionReference movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        Vector2 direction = movement.action.ReadValue<Vector2>().normalized;
        Vector2 moveAmount = moveSpeed * Time.deltaTime * direction;

        rb.position += moveAmount;
    }
}
