using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI score;

    private int s;

    void Update()
    {
        score.text = s.ToString();
        if (s > 9)
        {
            if (gameObject.tag == "Player1")
                score.text = "Player 1 Wins!";
                
            else
                score.text = "Player 1 Wins!";

            returnToMenu();
        }
    }

    private void returnToMenu()
    {
        StartCoroutine("goToMenu");
    }

    IEnumerator goToMenu()
    {
        yield return new WaitForSeconds(3f);
        // TODO: Add a way for players to return back to the board
        EventManager em = FindObjectOfType<EventManager>();
        em.LoadBoardMapTrigger();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bomb")
        {
            Destroy(collision.gameObject);
            if (s > 0)
                s--;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Box")
        {
            Destroy(collision.gameObject);
            s++;
        }
    }
}
