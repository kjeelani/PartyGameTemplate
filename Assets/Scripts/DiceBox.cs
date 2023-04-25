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
    private AudioSource audioS;
    private AudioClip diceRollSound;
    private AudioClip diceFinishSound;

    private bool clicked = false;

    //Events
    public delegate void DiceEvent(int num);
    //Event that minigame is being loaded
    public static event DiceEvent OnDiceRolled;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        sfx = GetComponent<ParticleSystem>();

        audioS = GetComponent<AudioSource>();
        diceRollSound = Resources.Load<AudioClip>("DiceRollSound");
        diceFinishSound = Resources.Load<AudioClip>("DiceFinishSound");

        diceSides = Resources.LoadAll<Sprite>("DiceSides/NewSides");
        diceAnimation = Resources.LoadAll<Sprite>("DiceAnimation/");

        transform.localScale = Vector3.zero;
        PlayerMovement.DicePopUp += PopUp;
        PlayerMovement.DiceVanish += Vanish;
    }

    private void OnDisable()
    {
        PlayerMovement.DicePopUp -= PopUp;
        PlayerMovement.DiceVanish -= Vanish;
    }

    private void PopUp()
    {
        transform.DOScale(1.6f, 0.5f).SetEase(Ease.OutBack);
        coroutineAllowed = true;
        clicked = false;
    }

    private void Vanish()
    {
        transform.DOScale(0f, 0.3f).SetEase(Ease.InBack);
    }

    private void OnMouseDown()
    {
        if (coroutineAllowed && !PlayerMovement.isAnyMoving)
        {
            StartCoroutine("DiceRoll");
            coroutineAllowed = false;
            clicked = true;
        }

    }

    private IEnumerator DiceRoll()
    {
        numberRolled = Random.Range(1, 7);
        audioS.PlayOneShot(diceRollSound);
        for (int i = 0; i < 19; i++)
        {
            //Bounce effect near the end
            if (i == 15) transform.DOPunchScale(new Vector3(0.7f, 0.7f, 0.7f), 0.4f, 0, 0);

            //play dice sound every 5 frames
            if (i % 5 == 0) { audioS.PlayOneShot(diceRollSound); }

            rend.sprite = diceAnimation[(i % 5)];
            yield return new WaitForSeconds(0.08f);
        }
        audioS.PlayOneShot(diceFinishSound);
        //Star particle play once number landed
        sfx.Play();
        // Number Rolled - 1 to get zero index of sprite array
        rend.sprite = diceSides[numberRolled - 1];

        //Broadcast Dice has been rolled
        OnDiceRolled(numberRolled);

    }

    private void OnMouseEnter()
    {
        if(clicked == false)
        {
            transform.DOScale(1.85f, 0.3f).SetEase(Ease.OutCubic);
        }
        
    }
    private void OnMouseExit()
    {
        if (clicked == false)
        {
            transform.DOScale(1.6f, 0.2f).SetEase(Ease.InCubic);
        }
        
    }
}
