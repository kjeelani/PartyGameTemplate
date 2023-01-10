using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will be attatched to a psuedo-game object (i.e invisible) and will be loaded once the game is started
public class BoardManager : MonoBehaviour
{
    private static GameObject player1, player2;
    public static int diceSideThrown = 0;
    public static int player1StartNode = 0;
    public static int player2StartNode = 0;
    public static bool gameOver = false; 

    // Use this for initialization
    void Start () {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");        
        player1.GetComponent<PlayerMovement>().moveAllowed = false;
        player2.GetComponent<PlayerMovement>().moveAllowed = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement player1Data = player1.GetComponent<PlayerMovement>();
        PlayerMovement player2Data = player2.GetComponent<PlayerMovement>();
        if (player1Data.nodeIndex > player1StartNode + diceSideThrown){
            if(player1Data.moveAllowed){
                doNodeAction(player1Data);
            }
            player1Data.moveAllowed = false;
            player1StartNode = player1Data.nodeIndex - 1;
        }

        if (player2Data.nodeIndex > player2StartNode + diceSideThrown){
            if(player2Data.moveAllowed){
                doNodeAction(player2Data);
            }
            player2Data.moveAllowed = false;
            player2StartNode = player2Data.nodeIndex - 1;
        }

        if (player1Data.nodeIndex == player1Data.nodes.Length){
            gameOver = true;
        }

        if (player2Data.nodeIndex == player2Data.nodes.Length){
            gameOver = true;
        }
    }

    public static void MovePlayer(int playerToMove)
    {
        switch (playerToMove) { 
            case 1:
                player1.GetComponent<PlayerMovement>().moveAllowed = true;
                break;
            case 2:
                player2.GetComponent<PlayerMovement>().moveAllowed = true;
                break;
        }
    }

    public void doNodeAction(PlayerMovement player){
        Debug.Log(player.currentNodeType);
        ////Unimplemented
        //player.currentNodeType.doAction();
    }
}
