using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    private void Start() {
        transform.position = nodes[nodeIndex].transform.position;
        currentNodeType = nodes[nodeIndex].gameObject;
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
        tileSound = Resources.Load<AudioClip>("TileSound");
        coinSound = Resources.Load<AudioClip>("CoinSound");
        minigameSound = Resources.Load<AudioClip>("MinigameSound");
    }

    public void Move() {
        playerCamera.Priority = 25;
        StartCoroutine("MoveToNextNode");
    }

    IEnumerator MoveToNextNode()
    {
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
            }
        }
        isAnyMoving = false;
        anim.SetBool("Moving", false);
        playerCamera.Priority = 0;
        NodeAction();
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
            audioS.PlayOneShot(coinSound);
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
}
