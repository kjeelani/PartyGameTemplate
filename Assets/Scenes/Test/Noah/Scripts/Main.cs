using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
    private int currentPlayer = 0;
    private int numAlivePlayers = 4;
    private const int totalPlayers = 4;
    private int[] playerIDs;
    private int playerIndex = 0;
    private int[] bombs;
    private void Start() {
        bombs = generateBombs(numAlivePlayers + 1);
        playerIDs = new int[totalPlayers];
        for (int i = 0; i < totalPlayers; i++) {
            playerIDs[i] = i;
        }

        foreach (var bomb in bombs) {
            Debug.Log(bomb);
        }
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

    private void pressButton(int buttonNumber) {
        // If this button has not been selected before
        if (bombs[buttonNumber] == 0) {
            Debug.Log("PLAYER " + (currentPlayer + 1) + ": Button " + (buttonNumber + 1) + " has no bomb");
            bombs[buttonNumber] = -1;
            incrementCurrentPlayer();
        }

        if (bombs[buttonNumber] == 1) {
            Debug.Log("PLAYER " + (currentPlayer + 1) + ": Button " + (buttonNumber + 1) + " has the bomb!");
            Debug.Log("ROUND OVER");
        }
    }

    // This function can be accessed from the Unity engine
    public void firstButton() {
        pressButton(0);
    }
    
    public void secondButton() {
        pressButton(1);
    }
    
    public void thirdButton() {
        pressButton(2);
    }
    
    public void fourthButton() {
        pressButton(3);
    }
    
    public void fifthButton() {
        pressButton(4);
    }

    private void incrementCurrentPlayer() {
        playerIndex++;
        if (playerIndex == numAlivePlayers) {
            playerIndex = 0;
        }

        currentPlayer = playerIDs[playerIndex];
    }
}
