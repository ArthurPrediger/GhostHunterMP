using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Personagem : MonoBehaviour
{
    [SerializeField]
    private float velocidadePersonagem;
    [SerializeField]
    private float velocidadeRotacao;

    private float RayLength = 100f;
    private Controles_Personagem playerActions;
    private Rigidbody rb;
    private Vector3 moveInput;
    private LayerMask floor;

    void Awake()
    {
        floor = LayerMask.GetMask("Floor");

        playerActions = new Controles_Personagem();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerActions.movimentacao.Enable();
    }

    private void OnDisable()
    {
        playerActions.movimentacao.Disable();
    }

    private void FixedUpdate()
    {
        Mover(playerActions.movimentacao.Mover.ReadValue<Vector2>());
    }

    private void Mover(Vector2 move)
    {
        moveInput = new Vector3(move.x, 0, move.y);

        rb.velocity = moveInput * velocidadePersonagem;

        if(move != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * velocidadeRotacao);
        }
    }
}
