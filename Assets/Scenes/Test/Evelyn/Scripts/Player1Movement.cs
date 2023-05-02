using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    private Rigidbody2D myBody;

    public float speed, xBound; //5, 8.5

    public float startXPos = -7;

    private Animator anim;

    void Start()
    {
        this.transform.position = new Vector2(startXPos, this.transform.position.y);
        myBody = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("HorizontalADKeys");

        //get Bucket_Move animation float
        anim.SetFloat("Bucket Move", h);

        if (h > 0)
            myBody.velocity = Vector2.right * speed;
        else if (h < 0)
            myBody.velocity = Vector2.left * speed;
        else
            myBody.velocity = Vector2.zero;

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -xBound, xBound), transform.position.y);
    }
}
