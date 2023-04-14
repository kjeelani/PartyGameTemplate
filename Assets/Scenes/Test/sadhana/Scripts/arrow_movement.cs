using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_movement : MonoBehaviour
{
    Rigidbody2D Rb;

    public float Speed;
    float MovementX;
    float MovementY;
    
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        MovementX = 0;
        MovementY = 0;
        //Speed = 6.75F;
    }

    // Update is called once per frame
    void Update()
    {
        Rb.velocity = new Vector2(MovementX * Speed * Time.deltaTime, MovementY * Speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovementY = 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MovementY = -1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovementX = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovementX = -1;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)) 
        {
            MovementY = 0;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            MovementX = 0;
        }
    }
}
