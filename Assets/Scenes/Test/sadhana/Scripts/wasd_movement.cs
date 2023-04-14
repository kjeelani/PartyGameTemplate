using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasd_movement : MonoBehaviour
{
    // Start is called before the first frame update
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

        if (Input.GetKeyDown(KeyCode.W))
        {
            MovementY = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MovementY = -1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MovementX = 1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MovementX = -1;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            MovementY = 0;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            MovementX = 0;
        }

    }
}
