using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MGEndScreenController : MonoBehaviour
{
    public TMP_Text resaultText, titleText;
    public Button resrartBtn, exitBtn;

    [Tooltip("Всё, что должно показываться только в конце игры")]
    public GameObject[] resultObjects;   

    public void Initialize(int score, float timeRemaining, bool victory)
    {
        resaultText.text = string.Format("Ты заработал {0} очков!", score, timeRemaining);
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