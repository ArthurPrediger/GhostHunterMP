using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ghost : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 16.0f;
    Vector2 dashDirection = Vector2.zero;
    public float dashSpeed = 8.0f;
    private Animator animator;
    bool canMove = true;
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
        DashAction();
    }

    public void MoveAction()
    {
        if (!movement.action.IsPressed() || !canMove) return;

        Vector2 direction = movement.action.ReadValue<Vector2>().normalized;
        Vector2 moveAmount = moveSpeed * Time.deltaTime * direction;

        rb.position += moveAmount;

        if(Mathf.Abs(direction.x) > 0f && direction.y == 0f)
        {
            animator.SetInteger("Rotation", 2);

            if(direction.x < 0f) transform.localScale = new Vector3(-1f, 1f, 1f);
            else transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if(direction.y > 0f)
        {
            animator.SetInteger("Rotation", 1);
        }
        else if(direction.y < 0f)
        {
            animator.SetInteger("Rotation", 0);
        }

        dashDirection = direction;
    }

    public void AttackAction()
    {
        bool attacking = attack.action.WasPressedThisFrame();

        if (!attacking || !canAttack) return;

        SetAllActionsState(false);

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

        SetAllActionsState(true);
    }

    private void DashAction()
    {
        bool dashing = dash.action.WasPressedThisFrame();

        if (!dashing || !canDash) return;

        SetAllActionsState(false);

        animator.SetBool("IsDashing", true);
        StartCoroutine (DashUpdate());
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashUpdate() 
    {
        float dashDuration = 1.25f / 8f;
        float time = 0f;
        while(time < dashDuration)
        {
            rb.transform.position += dashSpeed * Time.deltaTime * (Vector3)dashDirection;
            time += Time.deltaTime;
            //UnityEngine.Debug.Log(time);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator DashCooldown()
    {
        float dashDuration = 1.25f / 8f;
        float waitActivation = dashDuration / 4.0f;

        yield return new WaitForSeconds(waitActivation);
        animator.SetBool("IsDashing", false);
        yield return new WaitForSeconds(dashDuration - waitActivation);

        SetAllActionsState(true);
        canDash = false;
        yield return new WaitForSeconds(0f);
        canDash = true;
    }

    void SetAllActionsState(bool state)
    {
        canMove = state;
        canAttack = state; 
        canDash = state;
    }
}
