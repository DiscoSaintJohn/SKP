using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{

    public PlayerState currentState;
    public float speed;
    private Rigidbody2D rb;
    private Vector3 change;
    private Animator animator;

    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Fire1") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }

        else if (currentState == PlayerState.walk)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo() 
    {
        animator.SetBool("Attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        rb.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
} 
