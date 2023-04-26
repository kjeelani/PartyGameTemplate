using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    private Rigidbody2D myBody;

    public float speed, xBound; //5, 8.5

    public float startXPos = 7;

    void Start()
    {
        this.transform.position = new Vector2(startXPos, this.transform.position.y);
        myBody = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("HorizontalArrowKeys");

        if (h > 0)
            myBody.velocity = Vector2.right * speed;
        else if (h < 0)
            myBody.velocity = Vector2.left * speed;
        else
            myBody.velocity = Vector2.zero;

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -xBound, xBound), transform.position.y);
    }
}
