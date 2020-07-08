using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public int damage;
    private Enemy enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
        enemy = hit.gameObject.GetComponent<Enemy>();
        if (hit != null && this.gameObject.CompareTag("PlayerAttack"))
        {
            StartCoroutine(KnockCo(hit));
        }
    }

    private IEnumerator KnockCo(Rigidbody2D hit)
    {
        this.gameObject.SetActive(true);
        enemy.currentState = EnemyState.stagger;
        Vector2 forceDirection = hit.transform.position - transform.position;
        Vector2 force = forceDirection * thrust;
        hit.AddForce(force);
        yield return new WaitForSeconds(0.3f);
        hit.velocity = Vector2.zero;
        enemy.currentState = EnemyState.idle;
        enemy.TakeDamage(damage);
        this.gameObject.SetActive(false);
    }
}
