using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator uiAnimator;
    public Animator uiAnimator1;
    public CharacterController2D controller;
    public Animator animator;
    public Rigidbody2D rb;
    float horizontalMove = 0f;
    public float runSpeed = 40f;

    // bool isGrounded = true;
    bool jump = false;
    bool crouch = false;
    bool hasDied = false;

    Vector2 spawnLocation;

    void Start()
    {
        spawnLocation = transform.position;
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));


        if (Input.GetButtonDown("Jump"))
        {
            // isGrounded = false;
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            animator.SetTrigger("Crouch");
            crouch = true;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(Delay());
        }

        if (transform.position.y <= -12f && !hasDied)
        {
            hasDied = true;
            if (FindObjectOfType<PlayerHealth>().playerHealth <= 1)
            {
                Debug.Log("You Died!!");
                FindObjectOfType<PlayerHealth>().playerHealth--;
                // You died animation window and restart
                uiAnimator1.SetTrigger("Endgame");
            }
            else
            {

                StartCoroutine(Respawn());
            }


        }



    }
    IEnumerator False()
    {
        yield return new WaitForSeconds(1f);
        uiAnimator.SetBool("Fade", false);
    }

    IEnumerator Respawn()
    {
        uiAnimator.SetBool("Fade", true);
        yield return new WaitForSeconds(1f);
        FindObjectOfType<PlayerHealth>().playerHealth--;
        transform.position = spawnLocation;
        hasDied = false;
        StartCoroutine(False());
    }

    // void OnCollisionEnter(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Ground")
    //     {
    //         isGrounded = true;
    //     }
    // }
    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        crouch = false;

    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.6f);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<GameManager>().coinCount++;
        }
        if (other.gameObject.CompareTag("END"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
}

