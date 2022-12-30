using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private Animation ani;

    //Event that loading has finished
    public delegate void AnimationFinished();
    public static event AnimationFinished OnAnimationFinished;
    
    void Start()
    {
        //Get animation component via script
        if (!ani) ani = GetComponent<Animation>();

        //Subscribe to load minigame event : play loading animation if event is called
        EventManager.OnLoadMinigame += PlayAnimation;
    }

    private void OnDisable()
    {
        //Unsubcribe from load minigame event
        EventManager.OnLoadMinigame -= PlayAnimation;
    }

    void PlayAnimation() { ani.Play(); }

    void AnimationFinishedTrigger()
    {
        //Broadcast event that loading animation finished
        OnAnimationFinished();
    }
}
