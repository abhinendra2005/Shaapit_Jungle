using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Animator animator;
    public Animator uiAnimator;
    public Rigidbody2D rb;
    public int coinCount;
    public TMP_Text coinText;

    public void EndGame()
    {
        Debug.Log("You Died!");

        // Dying animation
        animator.SetBool("isDead",true);

        // disable movement
        FindObjectOfType<PlayerMovement>().enabled = false;
        FindObjectOfType<PlayerCombat>().enabled = false;
        rb.velocity = Vector2.zero;

        // ignore collision enemy and player
        Physics2D.IgnoreLayerCollision(6, 8);

        
        // "You died" screen and restart button
        StartCoroutine(Died());
    }

    IEnumerator Died()
    {
        yield return new WaitForSeconds(2f);
        uiAnimator.SetTrigger("Endgame");
    }

    void Update()
    {
        coinText.text = coinCount.ToString();
    }

    

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
