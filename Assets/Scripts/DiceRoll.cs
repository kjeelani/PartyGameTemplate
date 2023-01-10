using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    private int turn = 1; //1 for Player 1, -1 for Player 2
    private bool coroutineAllowed = true;

	// Use this for initialization
	private void Start () {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        rend.sprite = diceSides[5];
	}

    private void OnMouseDown()
    {
        if (!GameControl.gameOver && coroutineAllowed)
            StartCoroutine("RollTheDice");
    }

    private IEnumerator RollTheDice()
    {
        coroutineAllowed = false;
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }

        BoardManager.diceSideThrown = randomDiceSide + 1;
        if (turn == 1)
        {
            BoardManager.MovePlayer(1);
        } else if (turn == -1)
        {
            BoardManager.MovePlayer(2);
        }
        turn *= -1;
        coroutineAllowed = true;
    }
}
