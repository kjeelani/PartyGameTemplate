using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class wasd_movement : MonoBehaviour
{
    Rigidbody2D Rb;
    
    public float neutralSpeed;
    public float fastSpeed;
    public float slowSpeed;
    public float statusTimeInSeconds;
    float currentSpeed;
    float MovementX;
    float MovementY;

    private Animator anim;
    private AudioSource audioS;
    public GameObject headerText;
    
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        MovementX = 0;
        MovementY = 0;
        currentSpeed = neutralSpeed;

        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Player 2 Wins!");

        StopAllCoroutines();
        if (collision.gameObject.tag == "SpeedSquare") {
            StartCoroutine(TempSpeedBuff(statusTimeInSeconds));
        } else if (collision.gameObject.tag == "SlowSquare") {
            StartCoroutine(TempSlowDebuff(statusTimeInSeconds));
        } else if (collision.gameObject.tag == "End") {
            Debug.Log("Player 2 Wins!");
            //TODO: Delete this and instead go back to the board
            returnToMenu();
        }
    }

    private void returnToMenu()
    {
        StartCoroutine("goToMenu");
    }

    IEnumerator goToMenu()
    {
        headerText.GetComponent<TextMeshProUGUI>().text = "PLAYER 2 WINS! (+5 coins)";
        headerText.SetActive(true);
        audioS.Play();
        PlayerPrefs.SetInt("P2Score", PlayerPrefs.GetInt("P2Score") + 5);
        yield return new WaitForSeconds(3f);
        // TODO: Add a way for players to return back to the board
        EventManager em = FindObjectOfType<EventManager>();
        em.LoadBoardMapTrigger();
    }

    IEnumerator TempSpeedBuff(float waitTime) {
        currentSpeed = fastSpeed;
        yield return new WaitForSeconds(waitTime);
        currentSpeed = neutralSpeed;
    }

    IEnumerator TempSlowDebuff(float waitTime) {
        currentSpeed = slowSpeed;
        yield return new WaitForSeconds(waitTime);
        currentSpeed = neutralSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        Rb.velocity = new Vector2(MovementX * currentSpeed, MovementY * currentSpeed);

        if (Input.GetKeyDown(KeyCode.W))
        {
            MovementY = 1;
            anim.SetBool("Moving", true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MovementY = -1;
            anim.SetBool("Moving", true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MovementX = 1;
            anim.SetBool("Moving", true);
            anim.SetBool("Right", true);
            anim.SetBool("Left", false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MovementX = -1;
            anim.SetBool("Moving", true);
            anim.SetBool("Left", true);
            anim.SetBool("Right", false);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            MovementY = 0;
            
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            MovementX = 0;

            anim.SetBool("Moving", false);
        }

    }
}
