using UnityEngine;
using System.Collections;


public class PlayerDamage : MonoBehaviour
{
    public int damage = 1;

    public float invincibleDuration = 3f;
    public float blinkInterval = 0.2f;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(damage);
        }
    }

    void TakeDamage(int damage)
    {
        if (FindObjectOfType<PlayerHealth>().playerHealth <= 0)
        {
            Debug.Log("Player Died!!");
        }
        FindObjectOfType<PlayerHealth>().playerHealth -= damage;
        animator.SetTrigger("isHurt");
        StartCoroutine(Invulnerable());

    }

    IEnumerator Invulnerable()
    {
        Physics2D.IgnoreLayerCollision(6, 8);
        float elapsed = 0f;
        if (FindObjectOfType<PlayerHealth>().playerHealth >= 1)
        {
            while (elapsed < invincibleDuration)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled; // Toggle visibility
                yield return new WaitForSeconds(blinkInterval);
                elapsed += blinkInterval;
            }
        }
        spriteRenderer.enabled = true;
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
    void Update()
    {
        if (FindObjectOfType<PlayerHealth>().playerHealth <= 0)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
