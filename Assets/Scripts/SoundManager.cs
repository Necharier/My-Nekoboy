using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    //public AudioSorсe[] audioSorсes;
    public AudioClip[] music, sfx;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogWarning("больше одного SoundManager в сцене");
    }
}
