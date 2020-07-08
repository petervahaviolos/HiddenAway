using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    private Vector2 homePosition;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        homePosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
        if ((Vector2)transform.position == homePosition)
        {
            ChangeState(EnemyState.idle);
            animator.SetBool("wakeup", false);
        }
    }

    void CheckDistance()
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius && Vector2.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector2 movement = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                transform.position = movement;
                ChangeState(EnemyState.walk);
                ChangeAnimation((Vector2)target.position - movement);
                animator.SetBool("wakeup", true);
            }
        }
        else
        {
            Vector2 movement = Vector2.MoveTowards(transform.position, homePosition, moveSpeed * Time.deltaTime);
            ChangeState(EnemyState.walk);
            ChangeAnimation(homePosition - movement);
            transform.position = movement;
        }
    }

    private void ChangeAnimation(Vector2 direction)
    {
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
    }
}
