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
    private float currentDuration = 0.5f;
    private bool isDrumrolling = false;
    private int numSlots = 6;
    private bool gameOver = false;
    private int[] playerIDs;
    private int playerIndex = 0, startingPlayerIndex = 0;
    private int round = 1;
    private int numCorrectButtons = 0;
    private int numWinsToEndGame = 2;
    private int[] bombs;
    [SerializeField] private GameObject header, numRounds, button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, p1Wins, p2Wins;
    private TextMeshProUGUI headerText, numRoundsText, p1WinsText, p2WinsText;
    private int numP1Wins, numP2Wins;

    // Character related variables
    public GameObject player1, player2;
    private Vector3 leftSide, middle, rightSide, startingP1, endingP1, startingP2, endingP2;
    private bool isMovingP1, isMovingP2;
    private float lerpTimePlayer1, lerpTimePlayer2;
    private float moveSpeed = 0.8f;

    private Animator animatorPlayer1, animatorPlayer2;

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
        leftSide = new Vector3(-12, 0.4f, -5f);
        middle = new Vector3(0f, 0.4f, -5f);
        rightSide = new Vector3(12, 0.4f, -5f);

        player1.transform.position = leftSide;
        player2.transform.position = leftSide;

        animatorPlayer1 = player1.GetComponent<Animator>();
        animatorPlayer2 = player2.GetComponent<Animator>();

        startingP1 = leftSide;
        endingP1 = middle;
        isMovingP1 = true;

        headerText = header.GetComponent<TextMeshProUGUI>();
        numRoundsText = numRounds.GetComponent<TextMeshProUGUI>();
        p1WinsText = p1Wins.GetComponent<TextMeshProUGUI>();
        p2WinsText = p2Wins.GetComponent<TextMeshProUGUI>();
        
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

    private void Update() {
        if (isMovingP1) {
            moveAllPlayers(true);
            movePlayers(0, startingP1, endingP1);
        }

        if (isMovingP2) {
            moveAllPlayers(true);
            movePlayers(1, startingP2, endingP2);
        }
    }

    private void moveAllPlayers(bool val) {
        animatorPlayer1.SetBool("Moving", val);
        animatorPlayer2.SetBool("Moving", val);
        
        animatorPlayer1.SetBool("Right", val);
        animatorPlayer2.SetBool("Right", val);
    }

    private void movePlayers(int playerNumber, Vector3 starting, Vector3 ending) {
        if (playerNumber == 0) {
            lerpTimePlayer1 += Time.deltaTime;
            if (lerpTimePlayer1 >= moveSpeed) {
                lerpTimePlayer1 = moveSpeed;
            }

            var perc = lerpTimePlayer1 / moveSpeed;
            player1.transform.position = Vector3.Lerp(starting, ending, perc);

            if (player1.transform.position == ending) {
                lerpTimePlayer1 = 0;
                isMovingP1 = false;
                moveAllPlayers(false);
            }
        } else {
            lerpTimePlayer2 += Time.deltaTime;
            if (lerpTimePlayer2 >= moveSpeed) {
                lerpTimePlayer2 = moveSpeed;
            }

            var perc = lerpTimePlayer2 / moveSpeed;
            player2.transform.position = Vector3.Lerp(starting, ending, perc);
            
            if (player2.transform.position == ending) {
                lerpTimePlayer2 = 0;
                isMovingP2 = false;
                moveAllPlayers(false);
            }
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

    private IEnumerator nextRound() {
        yield return new WaitForSeconds(4);

        if (numP1Wins >= numWinsToEndGame) {
            // Player 1 has enough wins to end the game
            headerText.text = "Player 1 Won The Game!";
            yield return new WaitForSeconds(3);
            returnToMenu();
        } else if (numP2Wins >= numWinsToEndGame) {
            // Player 2 has enough wins to end the game
            headerText.text = "Player 2 Won The Game!";
            yield return new WaitForSeconds(3);
            returnToMenu();
        } else {
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

            // We reset the positions of the players
            if (startingPlayerIndex == 0) {
                player1.transform.position = middle;
                player2.transform.position = leftSide;
            } else {
                player2.transform.position = middle;
                player1.transform.position = leftSide;
            }
        
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
    }

    private IEnumerator pressButton(int buttonNumber) {
        // Play the noise
        if (!isDrumrolling && !gameOver && !isMovingP1 && !isMovingP2) {
            AudioManager.playDrumroll(4.0f - currentDuration);
            isDrumrolling = true;
            yield return new WaitForSeconds(currentDuration);
            isDrumrolling = false;
            currentDuration += 0.5f;
        }

        Debug.Log("PRESSED");
        // If this button has not been selected before
        if (bombs[buttonNumber] == 0 && !gameOver && !isMovingP1 && !isMovingP2 && !isDrumrolling) {
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
                currentDuration = 0.5f;
                StartCoroutine(nextRound());
            } else {
                headerText.text = "PLAYER " + (currentPlayer + 1) + ": Button " + (buttonNumber + 1) + " has no bomb";
            }
            bombs[buttonNumber] = -1;

            if (currentPlayer == 0) {
                startingP1 = middle;
                endingP1 = rightSide;
                
                startingP2 = leftSide;
                endingP2 = middle;
                
                isMovingP1 = true;
                isMovingP2 = true;
            } else {
                startingP2 = middle;
                endingP2 = rightSide;
                
                startingP1 = leftSide;
                endingP1 = middle;
                
                isMovingP2 = true;
                isMovingP1 = true;
            }
            
            incrementCurrentPlayer();
        }

        // Player has selected the bomb
        if (bombs[buttonNumber] == 1 && !gameOver && !(isMovingP1 || isMovingP2)) {
            changeButtonColor(buttonNumber, Color.red);
            currentDuration = 0.5f;
            
            headerText.text = "GAME OVER!\nPLAYER " + (currentPlayer + 1) + ": Button " + (buttonNumber + 1) + " has the bomb!";
            numAlivePlayers--;
            
            playerIDs[currentPlayer] = -1;
            gameOver = true;

            // If player 1 picked the bomb
            if (currentPlayer == 0) {
                // Then player 2 wins
                numP2Wins++;
            } else {
                // If player 2 picked the bomb, player 1 wins
                numP1Wins++;
            }
            
            updateWinCounts();

            StartCoroutine(nextRound());
        }
    }

    private void updateWinCounts() {
        p1WinsText.text = "P1 Win Count: " + numP1Wins;
        p2WinsText.text = "P2 Win Count: " + numP2Wins;
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
        StartCoroutine(pressButton(buttonNumber));
    }

    private void incrementCurrentPlayer() {
        playerIndex++;
        if (playerIndex == numAlivePlayers) {
            playerIndex = 0;
        }

        currentPlayer = playerIDs[playerIndex];
    }

    /**
     *  This method is used to return back to the main menu. Feel free to edit this method
     */
    private void returnToMenu() {
        // TODO: Add a way for players to return back to the board
        EventManager em = FindObjectOfType<EventManager>();
        em.LoadBoardMapTrigger();
    }
}
