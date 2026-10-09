using UnityEngine;

public class BaesController : MonoBehaviour
{
    public Animator baesAnimator;

  public static BaesController instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogWarning("больше одного BaesController в сцене");
    }

}

        
            
            
