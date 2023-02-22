using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class DiceBox : MonoBehaviour
{
    private Sprite[] diceSides;
    private Sprite[] diceAnimation;
    private SpriteRenderer rend;
    private ParticleSystem sfx;
    private bool coroutineAllowed = true;
    private int numberRolled;

    private int turn = 1;

    //Events
    public delegate void DiceEvent(int num);
    //Event that minigame is being loaded
    public static event DiceEvent OnDiceRolled;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        sfx = GetComponent<ParticleSystem>();

        diceSides = Resources.LoadAll<Sprite>("DiceSides/NewSides");
        diceAnimation = Resources.LoadAll<Sprite>("DiceAnimation/");
    }

    private void OnMouseDown()
    {
        if (coroutineAllowed)
        {
            StartCoroutine("DiceRoll");
            coroutineAllowed = false;
        }
            
    }

    private IEnumerator DiceRoll()
    {
        numberRolled = Random.Range(1, 7);
        for (int i = 0; i < 19; i++)
        {
            //Bounce effect near the end
            if (i == 15) transform.DOPunchScale(new Vector3(0.7f, 0.7f, 0.7f), 0.4f, 0, 0);

            rend.sprite = diceAnimation[(i % 5)];
            yield return new WaitForSeconds(0.08f);
        }
        //Star particle play once number landed
        sfx.Play();
        // Number Rolled - 1 to get zero index of sprite array
        rend.sprite = diceSides[numberRolled - 1];

        //Broadcast Dice has been rolled
        OnDiceRolled(numberRolled);

        //temp solution, -we want to decouple this script and BoardManager later.
        if (turn == 1)
        {
            BoardManager.MovePlayer(1);
        }
        else if (turn == -1)
        {
            BoardManager.MovePlayer(2);
        }
        turn *= -1;

        coroutineAllowed = true;
    }
}
