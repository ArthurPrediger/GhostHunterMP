using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ghost : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 16.0f;
    private Animator animator;
    bool canAttack = true;
    bool canDash = true;

    [SerializeField]
    private InputActionReference movement;
    [SerializeField]
    private InputActionReference attack;
    [SerializeField]
    private InputActionReference dash;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveAction();
        AttackAction();
    }

    public void MoveAction()
    {
        Vector2 direction = movement.action.ReadValue<Vector2>().normalized;
        Vector2 moveAmount = moveSpeed * Time.deltaTime * direction;

        rb.position += moveAmount;

        if(Mathf.Abs(direction.x) > 0.0001f && Mathf.Abs(direction.y) < 0.0001f)
        {
            animator.SetInteger("Rotation", 2);

            if(direction.x < -0.0001f) transform.localScale = new Vector3(-1f, 1f, 1f);
            else transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if(direction.y > 0.0001f)
        {
            animator.SetInteger("Rotation", 1);
        }
        else if(direction.y < -0.0001f)
        {
            animator.SetInteger("Rotation", 0);
        }
    }

    public void AttackAction()
    {
        bool attacking = attack.action.WasPressedThisFrame();

        if (!attacking || !canAttack) return;

        canAttack = false;

        animator.SetBool("IsAttacking", true);
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        float attackDuration = animator.GetNextAnimatorStateInfo(0).length;
        float waitActivation = attackDuration / 4.0f;

        yield return new WaitForSeconds(waitActivation);
        animator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(attackDuration - waitActivation);
        canAttack = true;
    }
}
