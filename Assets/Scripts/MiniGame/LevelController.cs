//using System.Collections.Generic;
using UnityEngine;
 
public class LevelController : MonoBehaviour
{
    public MiniGameLevel Level;
 
    [Tooltip("Насколько ниже самого нижнего уровня можно упасть до проигрыша")]
    public float fallMargin = 0f;
    public MiniGameBase game;
    private Transform bae;
   
 
    private void Awake()
    {
        enabled = false; 
        game = GetComponent<MiniGameBase>();
    }
 
    public void SetBae(Transform target)
    {
        bae = target;
        enabled = bae && Level;
    }
 
    private void Update()
    {
        float baeY = bae.position.y;
        if (baeY < Level.begin.position.y - fallMargin)
        {
            enabled = false;             
            if (game) game.Fail();
        }
    }
 
}