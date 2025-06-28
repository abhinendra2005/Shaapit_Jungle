using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public int attackDamage = 20;
    public float attackrange = 0.5f;
    public LayerMask enemyLayers;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }   
        }
        
    }
    void Attack()
    {
        // Play an Attack animation
        animator.SetTrigger("Attack");

        // Detect enimies in range of them
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackrange, enemyLayers);

        // Attack them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackrange);
    }
}
