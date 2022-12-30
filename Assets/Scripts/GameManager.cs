using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool MinigameInProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnLoadMinigame += MinigameStart;
    }

    private void OnDisable()
    {
        EventManager.OnLoadMinigame -= MinigameStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MinigameStart()
    {
        MinigameInProgress = true;
        Debug.Log(MinigameInProgress);
    }
}
