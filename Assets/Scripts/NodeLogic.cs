using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLogic : MonoBehaviour
{   
    public Color[] nodeColors;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        //Default for Blank - MiniGame - Points - Finish is White - Orange - Cyan - Green
        nodeColors = new Color[]{new Color(1.0f,1.0f,1.0f,1.0f), new Color(1.0f,.65f,0f,1.0f), new Color(0f,1.0f,1.0f,1.0f), new Color(0f,0f,1.0f,0f)};
        sprite = GetComponent<SpriteRenderer>();
        switch (gameObject.tag) {
            case "MiniGame":
                sprite.color = nodeColors[1];
                break;
            case "Points":
                sprite.color = nodeColors[2];
                break;
            case "Finish":
                sprite.color = nodeColors[3];
                break;
            default:
                sprite.color = nodeColors[0];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
