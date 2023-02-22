using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDemo : MonoBehaviour
{
    // 5 is the default, but this speed is adjustable in the Inspector
    public float speed = 10f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
   
    private void Start()
    {
        //grab the rigidbody component automatically
        rb = gameObject.GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        // Time.deltaTime is used to ensure that the movement is smooth and not frame rate dependent.
        transform.position = transform.position + new Vector3(horizontal * speed * Time.deltaTime, 0f, 0f);

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Debug.Log("Jumping");
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    #region Ground Detection
    private bool IsGrounded()
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
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            // 'normal' is the normal vector of the border, this code will prevent the player from crossing the border
            Vector2 normal = collision.contacts[0].normal;
            Vector2 currentPosition = transform.position;
            transform.position = new Vector2(currentPosition.x - normal.x * 0.05f, currentPosition.y - normal.y * 0.05f);
        }
    }
}
