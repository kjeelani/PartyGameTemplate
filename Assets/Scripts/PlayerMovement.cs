using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] nodes;
    public CinemachineVirtualCamera playerCamera;

    public static bool isAnyMoving = false;

    [SerializeField]
    private float moveSpeed = 10f;

    public int nodeIndex = 0;

    public GameObject currentNodeType;

    private Animator anim;
    private AudioSource audioS;
    private AudioClip tileSound;
    private AudioClip coinSound;
    private AudioClip minigameSound;

    private GameObject DiceBubble;
    private GameObject dice;

    public delegate void PlayerEvent();
    public static event PlayerEvent SwitchTurns;
    public static event PlayerEvent DicePopUp;
    public static event PlayerEvent DiceVanish;

    public enum Player { P1, P2 }
    public Player whichPlayer;

    private void Start() {
        LoadData();
        
        anim = GetComponent<Animator>();

        DiceBubble = transform.GetChild(0).gameObject;
        dice = FindObjectOfType<DiceBox>().gameObject;

        audioS = GetComponent<AudioSource>();
        tileSound = Resources.Load<AudioClip>("TileSound");
        coinSound = Resources.Load<AudioClip>("CoinSound");
        minigameSound = Resources.Load<AudioClip>("MinigameSound");

        playerCamera.Priority = 0;

        if (whichPlayer == Player.P1) { EventManager.OnP1Turn += StartTurn; }
        if (whichPlayer == Player.P2) { EventManager.OnP2Turn += StartTurn; }
    }

    private void StartTurn()
    {

        Debug.Log(BoardManager.currentTurn);
        Debug.Log(whichPlayer);
        if ((BoardManager.currentTurn == BoardManager.Turn.P1 && whichPlayer == Player.P1)
            || (BoardManager.currentTurn == BoardManager.Turn.P2 && whichPlayer == Player.P2))
        {
            StartCoroutine("Focus");
        }
    }
    IEnumerator Focus()
    {
        yield return new WaitForSeconds(2f);
        playerCamera.Priority = 25;
        yield return new WaitForSeconds(1f);
        //dice bubble popup
        DiceBubble.transform.localScale = Vector3.zero;
        DiceBubble.transform.DOScale(1.5f, 0.5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1f);
        dice.transform.SetPositionAndRotation(DiceBubble.transform.position, dice.transform.rotation);
        DicePopUp();

    }

    private void LoadData()
    {
        if (whichPlayer == Player.P1)
        {
            nodeIndex = PlayerPrefs.GetInt("Player1Node", 0);
        }
        else if (whichPlayer == Player.P2)
        {
            nodeIndex = PlayerPrefs.GetInt("Player2Node", 0);
        }

        transform.position = nodes[nodeIndex].position;
        currentNodeType = nodes[nodeIndex].gameObject;
    }

    private void StopTurn()
    {
        StartCoroutine("UnFocus");
    }

    IEnumerator UnFocus()
    {
        isAnyMoving = false;
        anim.SetBool("Moving", false);
        playerCamera.Priority = 0;
        yield return new WaitForSeconds(2f);
        //Signify player 2's turn 
        NodeAction();

        if (whichPlayer == Player.P1) {
            PlayerPrefs.SetInt("Player1Node", nodeIndex);
        }
        else if (whichPlayer == Player.P2) {
            PlayerPrefs.SetInt("Player2Node", nodeIndex);
        }
    }

    public void Move() {
        
        StartCoroutine("MoveToNextNode");
    }

    IEnumerator MoveToNextNode()
    {
        yield return new WaitForSeconds(1f);
        DiceBubble.transform.DOScale(0f, 0.3f).SetEase(Ease.OutCubic);
        DiceVanish();
        yield return new WaitForSeconds(1f);
        isAnyMoving = true; // set the flag to indicate that the object is moving
        anim.SetBool("Moving", true);

        for (int i = 0; i < BoardManager.diceSideThrown; i++)
        {
            yield return new WaitForSeconds(0.3f); // wait for the specified delay

            Vector2 startPos = transform.position; // get the starting position of the object
            Vector2 endPos = nodes[nodeIndex + 1].position; // get the end position of the object

            float t = 0f; // initialize the time variable

            while (t < 1f)
            {
                t += Time.deltaTime * moveSpeed; // increment the time variable based on the speed of movement
                transform.position = Vector2.Lerp(startPos, endPos, t); // move the object towards the end position
                yield return null; // wait for the next frame
            }
            audioS.PlayOneShot(tileSound);

            nodeIndex++; // move to the next point in the array
            if (nodeIndex >= nodes.Length)
            {
                //reached the end
                Debug.Log("REACHED THE END");
                nodeIndex = 0; // if we've reached the end of the array, loop back to the beginning
                transform.position = nodes[nodeIndex].position;
            }
        }
        StopTurn();
        
    }

    private void NodeAction()
    {
        currentNodeType = nodes[nodeIndex].gameObject;
        NodeLogic Node = currentNodeType.GetComponent<NodeLogic>();
        Debug.Log(Node.nodeType);
        if (Node.nodeType == NodeLogic.NodeType.Minigame)
        {
            //Broadcast to game that player landed on minigame tile;
            EventManager.LandedOnMinigame();
            audioS.PlayOneShot(minigameSound);
        }
        if(Node.nodeType == NodeLogic.NodeType.Coin)
        {
            //Update playerScore
            EventManager.LandedOnCoin();
            audioS.PlayOneShot(coinSound);
            SwitchTurns();
        }
        else if (Node.nodeType == NodeLogic.NodeType.None)
        {
            SwitchTurns();
        }
        
    }

#region Character Move Animation Conditions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NodeLogic Node = collision.gameObject.GetComponent<NodeLogic>();
        if (Node != null)
        {
            if (Node.directionChange == NodeLogic.DirectionChange.Right)
            {
                anim.SetBool("Right", true);
                anim.SetBool("Left", false);
            }
            else if (Node.directionChange == NodeLogic.DirectionChange.Left)
            {
                anim.SetBool("Right", false);
                anim.SetBool("Left", true);
            }
        }
        
    }
    #endregion

    private void OnDisable()
    {
        EventManager.OnP1Turn -= StartTurn;
        EventManager.OnP2Turn -= StartTurn;
    }
}
