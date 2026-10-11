using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public MiniGameBase miniGameBase;
    private bool collected;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;
        if (!collision.CompareTag("Player")) return;

        collected = true;
        if(CompareTag("Money"))
            miniGameBase.ChangeScore(1);

        if(CompareTag("TimeAdder"))
            miniGameBase.ChangeTimer(5);

        Destroy(gameObject);
    }
}

