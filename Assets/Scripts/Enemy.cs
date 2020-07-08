using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}


public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    // Start is called before the first frame update
    private void Awake()
    {
        health = (int)maxHealth.initialValue;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
