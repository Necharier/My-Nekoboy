using UnityEngine;
using UnityEngine.UI;

public class MGEndScreenController : MonoBehaviour
{
    public Text resaultText, titleText;

    [Tooltip("Всё, что должно показываться только в конце игры")]
    public GameObject[] resultObjects;   // ResultText, TitleText, EndText

    public void Initialize(int score, float timeRemaining, bool victory)
    {
        resaultText.text = string.Format("Ты заработал {0} очков за {1:0.0} секунд", score, timeRemaining);
        titleText.text = victory ? "Ты выиграл!" : "Ты проиграл(((";
        Show();
    }

    public void Show() => SetResultVisible(true);
    public void Hide() => SetResultVisible(false);

    private void SetResultVisible(bool visible)
    {
        foreach (var go in resultObjects)
            if (go) go.SetActive(visible);
    }
}