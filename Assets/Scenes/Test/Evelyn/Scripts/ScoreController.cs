using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI score, header;

    private int s;

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
        if (collision.gameObject.CompareTag("Bomb"))
        {
            Destroy(collision.gameObject);
            if (s > 0)
                s--;
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            Destroy(collision.gameObject);
            s++;

            score.text = s.ToString();
            if (s > 9)
            {
                if (gameObject.tag == "Player1")
                {
                    header.text = "Player 1 Wins! (+5 coins)";
                    PlayerPrefs.SetInt("P1Score", PlayerPrefs.GetInt("P1Score") + 5);
                }
                    
                else
                {
                    header.text = "Player 2 Wins! (+5 coins)";
                    PlayerPrefs.SetInt("P2Score", PlayerPrefs.GetInt("P2Score") + 5);
                } 

                returnToMenu();
            }
        }
    }
}
