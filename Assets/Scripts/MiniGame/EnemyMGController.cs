using UnityEngine;

public class EnemyMGController : MonoBehaviour
{
   protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
          if(collision.collider.CompareTag("Player"))
        {
            MiniGameUiController.instance.LoseMiniGame();
        }
    }
}
