using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // damage animation
        animator.SetTrigger("hurt");

        if (currentHealth < 0 || currentHealth == 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy died!!");

        // Die animation
        animator.SetBool("isDead", true);

        // Disable the Enemy
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<EnemyPatrol>().enabled = false;
        gameObject.layer = LayerMask.NameToLayer("DeadEnemy");
        StartCoroutine(DisableAfterDelay());
        this.enabled = false;
    }

    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false); // Disables the entire enemy
    }
}

