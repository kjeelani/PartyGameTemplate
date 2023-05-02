using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will be attatched to a psuedo-game object (i.e invisible) and will be loaded once the game is started
public class BoardManager : MonoBehaviour
{
    private EventManager em;
    private static GameObject player1, player2;
    public static int diceSideThrown = 0;
    public static int player1StartNode = 0;
    public static int player2StartNode = 0;
    public static bool gameOver = false;

    public enum Turn { P1, P2};
    public static Turn currentTurn = Turn.P1;

    private PlayerMovement p1;
    private PlayerMovement p2;

    void Start () {
        em = GameObject.Find("Event Manager").GetComponent<EventManager>();
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        p1 = player1.GetComponent<PlayerMovement>();
        p2 = player2.GetComponent<PlayerMovement>();

        DiceBox.OnDiceRolled += UpdateDiceMove;
        EventManager.OnLandMinigame += StartMinigame;

        PlayerMovement.SwitchTurns += UpdateTurns;
        StartCoroutine("InitialDelay");
    }

    IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(1f);
        if (currentTurn == Turn.P1) { em.NowP1Turn(); }
        else if (currentTurn == Turn.P2) { em.NowP2Turn(); }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void StartMinigame()
    {
        //Signify that a minigame was landed on
        //Choose Random minigame
        //Load that minigame
        em.LoadMinigameTrigger();
    }

    private void UpdateTurns()
    {
        if (currentTurn == Turn.P1)
        {
            em.NowP1Turn();
        }
        else if (currentTurn == Turn.P2)
        {
            em.NowP2Turn();
        }
    }

    private void UpdateDiceMove(int number)
    {
        diceSideThrown = number;
        if (currentTurn == Turn.P1){
            p1.Move();
            currentTurn = Turn.P2;
        }
        else if (currentTurn == Turn.P2) {
            p2.Move();
            currentTurn = Turn.P1;
        }
    }

    private void OnDisable()
    {
        //Unsubscribe from event
        DiceBox.OnDiceRolled -= UpdateDiceMove;
        EventManager.OnLandMinigame -= StartMinigame;
        PlayerMovement.SwitchTurns -= UpdateTurns;
    }
}
