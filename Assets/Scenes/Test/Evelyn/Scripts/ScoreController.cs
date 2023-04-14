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
        if (s > 2)
        {
            if (gameObject.tag == "Player1")
                Debug.Log("Player 1 Wins!");
            else
                Debug.Log("Player 2 Wins!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
