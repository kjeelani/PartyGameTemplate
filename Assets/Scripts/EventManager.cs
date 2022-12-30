using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    //Event that minigame is bieng loaded
    public delegate void LoadMinigame();
    public static event LoadMinigame OnLoadMinigame;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDisable()
    {
        LoadingScreen.OnAnimationFinished -= LoadMinigameFinished;
    }

    public void LoadMinigameTrigger()
    {
        // Broadcast the event that we are loading a minigame
        OnLoadMinigame();
        //Subscribe (aka listen) to the event that the loading screen animation has finished
        LoadingScreen.OnAnimationFinished += LoadMinigameFinished;
    }

    /*This function will be called when the "loading screen 
      animation finished" event has been broadcasted    */
    private void LoadMinigameFinished()
    {
        //string minigame = SelectMinigame(); <-- Uncomment after Task 1 Implemented

        SceneManager.LoadScene("TestMinigame");
    }

    #region Task 1
    /***TO-DO***
     Implement SelectMinigame(), a semi-random selection algorithim to determine minigame.
     First ever selection is random, then all folowing minigame selections should have some
     way of prioritizing unplayed minigames, while retaining a somewhat random nature.   */

    private string SelectMinigame()
    {
        return null;
    }
    #endregion
}
