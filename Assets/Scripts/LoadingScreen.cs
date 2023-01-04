using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private Animation ani;

    //Event that loading has finished
    public delegate void AnimationFinished();
    public static event AnimationFinished OnAnimationFinished;

    public enum Type { Minigame, BoardMap, Title};
    public Type ScreenType;
    
    void Start()
    {
        //Get animation component via script
        if (!ani) ani = GetComponent<Animation>();

        //Subscribe to load minigame event : play loading animation if event is called
        if (ScreenType == Type.Minigame) EventManager.OnLoadMinigame += PlayAnimation;

        //Subscribe to load minigame event : play loading animation if event is called
        if (ScreenType == Type.BoardMap) EventManager.OnLoadBoardMap += PlayAnimation;

        //Subscribe to load title screen : play loading animation if event is called
        if (ScreenType == Type.Title) EventManager.OnLoadTitle += PlayAnimation;
    }

    private void OnDisable()
    {
        //Unsubcribe from event when object is diabled
        if (ScreenType == Type.Minigame) EventManager.OnLoadMinigame -= PlayAnimation;
        if (ScreenType == Type.BoardMap) EventManager.OnLoadBoardMap -= PlayAnimation;
        if (ScreenType == Type.Title) EventManager.OnLoadTitle -= PlayAnimation;
    }

    void PlayAnimation() { ani.Play(); }

    void AnimationFinishedTrigger()
    {
        //Broadcast event that loading animation finished
        OnAnimationFinished();
    }
}
