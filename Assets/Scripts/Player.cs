using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger
}

public class Player : MonoBehaviour
{
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    public VectorValue startingPosition;

    public float speed;
    private Rigidbody2D rbody;
    private Vector2 deltaDirection;
    private Animator animator;
    public PlayerState currentState;
    public GameObject attackPosition;

    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
    }

    void Update()
    {
        deltaDirection = Vector2.zero;
        deltaDirection.x = Input.GetAxisRaw("Horizontal");
        deltaDirection.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk)
        {
            UpdateAnimation();
        }
    }

    void FixedUpdate()
    {
        if (currentState == PlayerState.walk)
        {
            MoveCharacter();
        }
    }

    void UpdateAnimation()
    {
        if (deltaDirection != Vector2.zero)
        {
            animator.SetFloat("moveX", deltaDirection.x);
            animator.SetFloat("moveY", deltaDirection.y);
            animator.SetBool("moving", true);
            attackPosition.transform.position = ((Vector2)transform.position + 0.8f * deltaDirection);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        rbody.MovePosition((Vector2)transform.position + deltaDirection.normalized * speed * Time.deltaTime);
    }

    private IEnumerator AttackCo()
    {
        attackPosition.SetActive(true);
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);
        currentState = PlayerState.walk;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log($"Taking {enemy.baseAttack} damage");
            currentHealth.runtimeValue -= enemy.baseAttack;
            playerHealthSignal.RaiseSignal();
            if (currentHealth.runtimeValue <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
