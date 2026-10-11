using UnityEngine;
using TMPro;
 
public class MiniGameUiController : MonoBehaviour
{
    public static MiniGameUiController instance;
 
    [Header("Камера")]
    public CamerBehaviour camerBehaviour;
    public Camera mainCamera;
 
    [Header("Игрок")]
    public MiniGameBaesController miniGameBaesController;
    public Transform baeTransform;
 
    [Header("Мини-игра")]
    public MiniGameBase minigamePrefab;     
    private MiniGameBase currentGame;   
    public Transform levelParent;
    public Transform levelAnchor; 
 
    [Header("UI мини-игры")]
    public TMP_Text scoreText, timeText, endText;
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
       //Debug.Log("Уровень уничтожен\n" + System.Environment.StackTrace, this);
        if (instance == this) instance = null;
    }
     
 
    private void OnEnable()
    {
        if (instance != this) return;
        StartMiniGame();
    }
 
    private void OnDisable()
    {
       // Debug.Log("OnDisable контроллера\n" + System.Environment.StackTrace, this);
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
 
        var level = currentGame.GetComponentInChildren<LevelController>();
        if (level != null) level.SetBae(baeTransform);
 
 
        currentGame.ScoreChanged += UpdateScore;
        currentGame.TimerChanged += UpdateTimer;
        currentGame.Failed += OnGameFailed;
        currentGame.Finished += FinishingMinigame;
 
        miniGameBaesController.ResetState(currentGame.GetStartPosition());
        miniGameBaesController.enabled = true;        
        camerBehaviour.Follow(baeTransform);
    }
 
    private void CleanUp()
    {
        //Debug.Log("CleanUp вызван\n" + System.Environment.StackTrace, this);
        if (miniGameBaesController) miniGameBaesController.enabled = false;
 
        if (camerBehaviour) camerBehaviour.ResetCamera();
 
        if (currentGame)
        {
            currentGame.ScoreChanged -= UpdateScore;
            currentGame.TimerChanged -= UpdateTimer;
            currentGame.Failed -= OnGameFailed;
            currentGame.Finished -= FinishingMinigame;
            Destroy(currentGame.gameObject);   
        }
        currentGame = null;
    }
 
    public void PlayButton()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);   // OnEnable сам вызовет StartMiniGame
        else
            RestartMiniGame();            // объект уже включён, OnEnable не сработает
    }
 
    public void ExitButton()
    {
        CleanUp();
        if (endScreen != null) endScreen.Hide();
    }
    public void RestartMiniGame()              // для кнопки «Играть снова»
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
    private void OnGameFailed(int score, float timer) => LoseMiniGame();
 
    public void LoseMiniGame()
    {
        miniGameBaesController.enabled = false;   // остановить управление и физику игрока
 
        if (endScreen != null)
            endScreen.Initialize(score, timeRemaining, false);
 
        if (endText != null)
            endText.text = $"Счёт: {score}\nВремя: {timeRemaining:0.0}";
    }
}
 