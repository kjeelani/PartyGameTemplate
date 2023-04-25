using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static bool MinigameInProgress = false;

    public GameObject headerText;
    private GameObject p1ScoreText, p2ScoreText;
    private AudioSource audioS;

    [HideInInspector]
    public int player1Score;
    public int player2Score;

    private void Awake()
    {
        EventManager.OnP1Turn += SignalP1Turn;
        EventManager.OnP2Turn += SignalP2Turn;
        PlayerMovement.DiceVanish += CloseText;
    }

    // Start is called before the first frame update
    void Start()
    {
        //headerText = GameObject.Find("Header Text");

        EventManager.OnLoadMinigame += MinigameStart;
        EventManager.OnLandCoin += AddCoins;

        EventManager.OnLoadMinigame += FadeOutMusic;
        EventManager.OnLandMinigame += SignalMinigame;
        EventManager.OnLoadBoardMap += FadeOutMusic;
        EventManager.OnLoadTitle += FadeOutMusic;

        audioS = GetComponent<AudioSource>();
        FadeInMusic();

        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Test Board")
        {
            p1ScoreText = GameObject.Find("p1 coin text");
            p2ScoreText = GameObject.Find("p2 coin text");
            UpdatePlayerScores();
        }
        
    }

    private void OnDisable()
    {
        EventManager.OnLoadMinigame -= MinigameStart;
        EventManager.OnLoadMinigame -= FadeOutMusic;
        EventManager.OnLoadBoardMap -= FadeOutMusic;
        EventManager.OnLandMinigame -= SignalMinigame;
        EventManager.OnLandCoin -= AddCoins;
        EventManager.OnLoadTitle -= FadeOutMusic;
        EventManager.OnP1Turn -= SignalP1Turn;
        EventManager.OnP2Turn -= SignalP2Turn;
        PlayerMovement.DiceVanish -= CloseText;
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

    private void FadeInMusic()
    {
        //Fade in the music in 5 seconds
        audioS.Play();
        audioS.volume = 0;
        audioS.DOFade(0.35f, 8f);
    }

    private void FadeOutMusic()
    {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        if (audioS != null)
        {
            audioS.DOFade(0f, 1f);
            yield return new WaitForSeconds(1f);
            audioS.Stop();
        }
    }

    private void SignalP1Turn()
    {
        UpdateText("Player 1 Turn");
    }

    private void SignalP2Turn()
    {
        UpdateText("Player 2 Turn");
    }

    private void SignalMinigame()
    {
        UpdateText("Minigame!");
    }

    private void AddCoins()
    {
        //the player who just moved will have their points updated
        if (BoardManager.currentTurn == BoardManager.Turn.P2)
        {
            player1Score += 3;
            PlayerPrefs.SetInt("P1Score", player1Score);
        }
        else if (BoardManager.currentTurn == BoardManager.Turn.P1)
        {
            player2Score += 3;
            PlayerPrefs.SetInt("P2Score", player2Score);
        }
        UpdatePlayerScores();
    }
    private void UpdatePlayerScores()
    {
        player1Score = PlayerPrefs.GetInt("P1Score", 0);
        player2Score = PlayerPrefs.GetInt("P2Score", 0);

        p1ScoreText.GetComponent<TextMeshProUGUI>().text = player1Score.ToString();
        p2ScoreText.GetComponent<TextMeshProUGUI>().text = player2Score.ToString();
    }

    private void UpdateText(string t)
    {
        headerText.SetActive(true);
        TextMeshProUGUI text = headerText.GetComponent<TextMeshProUGUI>();

        //set the transarency off
        Color newColor = text.color;
        newColor.a = 0;

        //change the text
        text.text = t;
        text.color = newColor;
        headerText.transform.DOScale(1f, 0.6f).SetEase(Ease.OutBack);
        text.DOFade(1f, 0.4f);
        //StartCoroutine("updateText");
    }

    private void CloseText()
    {
        StartCoroutine("closeText");
    }
    
    IEnumerator closeText()
    {
        TextMeshProUGUI text = headerText.GetComponent<TextMeshProUGUI>();
        headerText.transform.DOScale(0f, 0.6f).SetEase(Ease.InBack);
        text.DOFade(0f, 0.4f);
        yield return new WaitForSeconds(1);
        headerText.SetActive(false);
    }
}
