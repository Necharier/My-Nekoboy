using UnityEngine;
using UnityEngine.UI;

public class MiniGameUiController : MonoBehaviour
{
    public static MiniGameUiController instance;
    public CamerBehaviour camerBehaviour;
    public Camera mainCamera;

    public MiniGameBaesController miniGameBaesController;
    public Transform baeTransform;

    public MiniGameBase minigamePrefab;     
    private MiniGameBase currentGame;   
    public Transform levelParent;
    public Transform levelAnchor; 

    public Text scoreText, timeText, endText;
    public MGEndScreenController endScreen;

    public float timeRemaining;              
    public int score;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Debug.LogWarning("Больше одного MiniGameUiController на сцене", this);
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }
     

    private void OnEnable()
    {
        if (instance != this) return;
        StartMiniGame();
    }

    private void OnDisable()
    {
        CleanUp();
    }


    private void StartMiniGame()
    {
        if (currentGame) return;

        if (endScreen != null) endScreen.Hide();

        UpdateScore(0);

        Transform parent = levelParent ? levelParent : transform;
        Vector3 pos = levelAnchor ? levelAnchor.position : parent.position;
        Quaternion rot = levelAnchor ? levelAnchor.rotation : Quaternion.identity;

        currentGame = Instantiate(minigamePrefab, pos, rot, parent);
       

        currentGame.ScoreChanged += UpdateScore;
        currentGame.TimerChanged += UpdateTimer;
        currentGame.Finished += FinishingMinigame;

        miniGameBaesController.ResetState(currentGame.GetStartPosition());
        miniGameBaesController.enabled = true;
        camerBehaviour.Follow(miniGameBaesController.transform);
    }

    private void CleanUp()
    {
        if (miniGameBaesController) miniGameBaesController.enabled = false;

        if (camerBehaviour) camerBehaviour.ResetCamera();

        if (currentGame)
        {
            currentGame.ScoreChanged -= UpdateScore;
            currentGame.TimerChanged -= UpdateTimer;
            currentGame.Finished -= FinishingMinigame;
            Destroy(currentGame.gameObject);   
        }
        currentGame = null;
    }

    public void PlayButton()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);   
        else
            RestartMiniGame();           
    }

    public void RestartMiniGame()              
    {
        CleanUp();
        StartMiniGame();
    }

    public void UpdateTimer(float timer)
    {
        timeRemaining = timer;
        timeText.text = $"Осталось: {timer:0.0}";
    }

    public void FinishingMinigame(int score, float timer)
    {
        miniGameBaesController.enabled = false;

        if (endScreen != null)
            endScreen.Initialize(score, timer, timer > 0);   

        if (endText != null)
            endText.text = $"Счёт: {score}\nВремя: {timer:0.0}";
    }
    public void UpdateScore(int score)
    {
        this.score = score;
        scoreText.text = "Счёт:" + score;
    }
    public void LoseMiniGame()
    {
        CleanUp();
        if (endScreen != null)
            endScreen.Initialize(score, timeRemaining, false);   
    }
}
