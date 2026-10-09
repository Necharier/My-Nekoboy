using UnityEngine;
using UnityEngine.UI;

public class PetUiController : MonoBehaviour
{
    public static PetUiController instance;

    public Image satietyScale, happinessScale, cleannessScale;
    public GameObject miniGame, mainGame;
    public float fillSpeed = 0.5f;   // доля шкалы в секунду

    private float satietyTarget, happinessTarget, cleannessTarget;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Debug.LogWarning("больше одного PetUiController в сцене");
    }

    void Start()
    {
        miniGame.SetActive(false);
        mainGame.SetActive(true);
    }

    
    public void UpdateScales(int happiness, int cleannes, int satiety)
    {
        happinessTarget = Mathf.Clamp01(happiness / 100f);
        cleannessTarget = Mathf.Clamp01(cleannes / 100f);
        satietyTarget   = Mathf.Clamp01(satiety / 100f);
    }

    void Update()
    {
        MoveScale(satietyScale, satietyTarget);
        MoveScale(happinessScale, happinessTarget);
        MoveScale(cleannessScale, cleannessTarget);
    }

    void MoveScale(Image scale, float target)
    {
        if (scale == null) return;
        scale.fillAmount = Mathf.MoveTowards(scale.fillAmount, target, fillSpeed * Time.deltaTime);
        scale.color = Color.Lerp(Color.red, Color.green, scale.fillAmount);
    }
}