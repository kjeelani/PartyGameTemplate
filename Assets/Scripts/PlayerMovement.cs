using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] nodes;

    [SerializeField]
    private float moveSpeed = 1f;

    [HideInInspector]
    public int nodeIndex = 0;
    public bool moveAllowed = false;

    public GameObject currentNodeType;

    private void Start() {
        transform.position = nodes[nodeIndex].transform.position;
        currentNodeType = nodes[nodeIndex].gameObject;
    }

    private void Update() {
        if (moveAllowed) {
            Move();
        }
    }

    private void Move() {
        if (nodeIndex <= nodes.Length - 1){
            transform.position = Vector3.MoveTowards(transform.position, nodes[nodeIndex].transform.position, moveSpeed * Time.deltaTime);
            
            if (transform.position == nodes[nodeIndex].transform.position){
                nodeIndex += 1;
                currentNodeType = nodes[nodeIndex].gameObject;
            }
        }
    }
}
