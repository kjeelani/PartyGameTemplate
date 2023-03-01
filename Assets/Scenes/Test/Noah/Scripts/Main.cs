using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
    private int currentPlayer = 0;
    private int numAlivePlayers = 2;
    private const int totalPlayers = 2;
    private int numSlots = 10;
    private bool gameOver = false;
    private int[] playerIDs;
    private int playerIndex = 0, startingPlayerIndex = 0;
    private int round = 1;
    private int numCorrectButtons = 0;
    private int[] bombs;
    [SerializeField] private GameObject header, numRounds, button1, button2, button3, button4, button5, button6, button7, button8, button9, button10;
    private TextMeshProUGUI headerText, numRoundsText;

    private Image buttonImage1,
        buttonImage2,
        buttonImage3,
        buttonImage4,
        buttonImage5,
        buttonImage6,
        buttonImage7,
        buttonImage8,
        buttonImage9,
        buttonImage10;
    private void Start() {
        headerText = header.GetComponent<TextMeshProUGUI>();
        numRoundsText = numRounds.GetComponent<TextMeshProUGUI>();
        
        bombs = generateBombs(numSlots);
        playerIDs = new int[totalPlayers];
        for (int i = 0; i < totalPlayers; i++) {
            playerIDs[i] = i;
        }

        foreach (var bomb in bombs) {
            Debug.Log(bomb);
        }

        // Used to change the color of the buttons later
        buttonImage1 = button1.GetComponent<Image>();
        buttonImage2 = button2.GetComponent<Image>();
        buttonImage3 = button3.GetComponent<Image>();
        buttonImage4 = button4.GetComponent<Image>();
        buttonImage5 = button5.GetComponent<Image>();
        buttonImage6 = button6.GetComponent<Image>();
        buttonImage7 = button7.GetComponent<Image>();
        buttonImage8 = button8.GetComponent<Image>();
        buttonImage9 = button9.GetComponent<Image>();
        buttonImage10 = button10.GetComponent<Image>();
    }

    private int[] generateBombs(int bombAmount) {
        var random = new System.Random();
        // Random is from lower (INCLUSIVE) to upper (EXCLUSIVE)
        var upperBound = bombAmount;
        var bombIndex = random.Next(0, upperBound);

        var bombList = new int[bombAmount];
        for (int i = 0; i < bombList.Length; i++) {
            if (i == bombIndex) {
                // There is a bomb
                bombList[i] = 1;
            } else {
                // There is no bomb
                bombList[i] = 0;
            }
        }
        // Example: [0, 1, 0, 0, 0]. The bomb is in index 1
        return bombList;
    }

    private IEnumerator nextRound() {
        yield return new WaitForSeconds(4);
        
        // Change everything to be white
        for (int i = 0; i < numSlots; i++) {
            changeButtonColor(i, Color.white);
        }
        
        // Generate new bombs
        bombs = generateBombs(numSlots);
        
        // Change the indices so that the other player now starts (for fairness)
        startingPlayerIndex = 1 - startingPlayerIndex;
        playerIndex = startingPlayerIndex;
        currentPlayer = startingPlayerIndex;
        
        // Reset player array
        for (int i = 0; i < totalPlayers; i++) {
            playerIDs[i] = i;
        }
        
        // Increment the round
        round++;
        
        // Change the texts
        headerText.text = "Choose a button!";
        numRoundsText.text = "Round: " + round;

        // Game is no longer over
        gameOver = false;

        // Reset the amount of players that are alive
        numAlivePlayers = totalPlayers;
        
        // Reset number of correct buttons
        numCorrectButtons = 0;
    }

    private void pressButton(int buttonNumber) {
        // If this button has not been selected before
        if (bombs[buttonNumber] == 0 && !gameOver) {
            numCorrectButtons++;
            changeButtonColor(buttonNumber, Color.green);

            // If we selected all the right buttons, reset
            if (numCorrectButtons == numSlots - 1) {
                // Keep the same round by subtracting by 1 since we will add by 1 in nextRound()
                round--;
                // Also keep the same starting player
                startingPlayerIndex = 1 - startingPlayerIndex;
                
                headerText.text = "RESTART ROUND\nPLAYER " + (currentPlayer + 1) + ": Button " + (buttonNumber + 1) + " has no bomb";
                gameOver = true;
                StartCoroutine(nextRound());
            } else {

                headerText.text = "PLAYER " + (currentPlayer + 1) + ": Button " + (buttonNumber + 1) + " has no bomb";
            }
            bombs[buttonNumber] = -1;
            incrementCurrentPlayer();
        }

        // Player has selected the bomb
        if (bombs[buttonNumber] == 1 && !gameOver) {
            changeButtonColor(buttonNumber, Color.red);
            
            headerText.text = "GAME OVER!\nPLAYER " + (currentPlayer + 1) + ": Button " + (buttonNumber + 1) + " has the bomb!";
            numAlivePlayers--;
            
            playerIDs[currentPlayer] = -1;
            gameOver = true;

            StartCoroutine(nextRound());
        }
    }

    private void changeButtonColor(int buttonNumber, Color color) {
        switch (buttonNumber) {
            case 0:
                buttonImage1.color = color;
                break;
            case 1:
                buttonImage2.color = color;
                break;
            case 2:
                buttonImage3.color = color;
                break;
            case 3:
                buttonImage4.color = color;
                break;
            case 4:
                buttonImage5.color = color;
                break;
            case 5:
                buttonImage6.color = color;
                break;
            case 6:
                buttonImage7.color = color;
                break;
            case 7:
                buttonImage8.color = color;
                break;
            case 8:
                buttonImage9.color = color;
                break;
            case 9:
                buttonImage10.color = color;
                break;
        }
    }

    // This function can be accessed from the Unity engine
    public void press(int buttonNumber) {
        pressButton(buttonNumber);
    }

    private void incrementCurrentPlayer() {
        playerIndex++;
        if (playerIndex == numAlivePlayers) {
            playerIndex = 0;
        }

        currentPlayer = playerIDs[playerIndex];
    }
}
