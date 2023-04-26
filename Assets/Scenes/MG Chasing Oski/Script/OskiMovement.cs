using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OskiMovement : MonoBehaviour
{
    public Rigidbody2D oski;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;


    private void Start()
    {
        oski = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        if (input < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (input > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        oski.velocity = new Vector2(input * speed, oski.velocity.y);
    }
}
