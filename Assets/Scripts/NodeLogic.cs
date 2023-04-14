using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLogic : MonoBehaviour
{
    // 0: blue tile, 1: red tile, 2: coin tile, 3: minigame tile
    public Sprite[] tileSprites;

    private SpriteRenderer sprite;

    public enum NodeType { Minigame, Coin, Finish, None };
    public enum DirectionChange { None, Right, Left};

    public NodeType nodeType;
    public DirectionChange directionChange;
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        //Default for Blank - MiniGame - Points - Finish is White - Orange - Cyan - Green
        
        sprite = GetComponent<SpriteRenderer>();
        switch (gameObject.tag) {
            case "MiniGame":
                sprite.sprite = tileSprites[3];
                nodeType = NodeType.Minigame;
                break;
            case "Coin":
                sprite.sprite = tileSprites[2];
                nodeType = NodeType.Coin;
                break;
            case "Finish":
                sprite.sprite = tileSprites[0];
                break;
            default:
                sprite.sprite = tileSprites[0];
                nodeType = NodeType.None;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
