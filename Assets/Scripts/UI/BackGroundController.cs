using UnityEngine;

public class BackGroundController : MonoBehaviour
{
   public Sprite[] one, two, three, four;
   public SpriteRenderer[] oneRenderer, twoRenderer, threeRenderer, fourRenderer;

   private void Start()
    {
        RandomizeBackgrounds();
    }

   public void RandomizeBackgrounds()
    {
        RendersGraphic(oneRenderer, one);
        RendersGraphic(twoRenderer,two);
        RendersGraphic(threeRenderer, three);
        RendersGraphic(fourRenderer, four);
    }
   
   public void RendersGraphic(SpriteRenderer[] spriteRenderers, Sprite[] sprites)
    {
        foreach(SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
        }
    }
}
