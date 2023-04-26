using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Student : MonoBehaviour
{
    public Rigidbody2D studentPlayer;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;
    public float jumpforce;

    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;

    public float jumpTime = 0.35f;
    public float jumpTimeCounter;
    private bool isJumping;

    private bool lost = false;
    private bool isPlayer1 = false;
    private string jumpAxis = "";
    private string horizontalAxis = "";

    //run once at start
    private void Start()
    {
        studentPlayer = GetComponent<Rigidbody2D>();
        if (this.tag == "Player1")
        {
            jumpAxis = "JumpP1";
            horizontalAxis = "HorizontalArrowKeys";
            isPlayer1 = true;
        } else
        {
            jumpAxis = "JumpP2";
            horizontalAxis = "HorizontalADKeys";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lost == false)
        {
            input = Input.GetAxisRaw(horizontalAxis);
            if (input < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (input > 0)
            {
                spriteRenderer.flipX = false;
            }

            //Jumping

            isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);

            if (isGrounded == true && Input.GetButtonDown(jumpAxis))
            {
                jumpTimeCounter = jumpTime;
                studentPlayer.velocity = Vector2.up * jumpforce;
            }

            if (Input.GetButton(jumpAxis) && isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    studentPlayer.velocity = Vector2.up * jumpforce;
                    jumpTimeCounter -= Time.deltaTime;
                }

                else
                {
                    isJumping = false;
                }

            }

            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }
        }
        

        //jumping
        /*if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {

            Debug.Log("Jumping");
            studentPlayer.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }*/
       
    }

    //Ground Detection
    /*private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1f;

        // Shoots a ray starting from the players position, this ray is 0.5 units long in the down direction,
        // if this ray hits the ground, indicated with its "Ground" layer, the player is grounded.
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }*/

    void FixedUpdate()
    {
        studentPlayer.velocity = new Vector2(input * speed, studentPlayer.velocity.y);    
    }

    private void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("void"))
        {
            loseGame();
        }
    }

    void loseGame()
    {
        lost = true;
    }
}
