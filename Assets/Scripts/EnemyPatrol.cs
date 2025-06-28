using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public Rigidbody2D rb;
    public Animator animator;
    public Transform currentPoint;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        // Move the enemy left or right based on current target point
        if (currentPoint == pointA.transform)
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(speed, 0);
        }

        // Switch target point when close enough
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            if (currentPoint == pointA.transform)
            {
                Flip();
                currentPoint = pointB.transform;
            }
            else if (currentPoint == pointB.transform)
            {
                Flip();
                currentPoint = pointA.transform;
            }
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

}
