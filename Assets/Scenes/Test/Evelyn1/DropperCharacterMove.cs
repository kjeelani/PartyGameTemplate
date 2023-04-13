using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperCharacterMove : MonoBehaviour
{
    private Vector2 screenBounds;

    public float moveSpeed = 5f;

    public Rigidbody rb;

    Vector3 movement; 
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + 1, screenBounds.x * -1 - 1);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + 1, screenBounds.y * -1 - 1);
        transform.position = viewPos;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
