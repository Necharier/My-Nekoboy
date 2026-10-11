using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
   private float startingHealth = 100f;
   public float currentHealth;
   private float scaleHealth;
   private float fillSpeed = 0.5f;
   public Image healthScale;
   //public Animator animator;
   private bool isDead;

    

    void Update()
    {
        if (healthScale == null) return;
        float target = currentHealth / startingHealth; 
        healthScale.fillAmount = Mathf.MoveTowards(healthScale.fillAmount, target, fillSpeed * Time.deltaTime);
        healthScale.color = Color.Lerp(Color.red, Color.green, healthScale.fillAmount);
    }

    private void Start()
    {
        
    }

    private void Awake()
    {
        currentHealth = startingHealth;
        //animator.GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        scaleHealth   = Mathf.Clamp01(currentHealth / 100f);

        if(currentHealth > 0)
        {
            //animator.SetTrigger("Hurting");
        }
        else
        {
            if(!isDead)
            {
                //animator.SetTrigger("Die");
                GetComponent<MiniGameBaesController>().enabled = false;
                isDead = true;
            }
            
        }
    }
}
