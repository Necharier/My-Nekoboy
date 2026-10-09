using UnityEngine;
using System;

public class NeedsController : MonoBehaviour
{
    public GameObject baehappy, baesad;
    public int satiety = 70;
    public int happiness = 70;
    public int cleannes = 70;
    public int satietyTickRate = 10, happinessTickRate = 10, cleannesTickRate = 10;
    public DateTime lastTimeFed, lastTimeHappy, lastTimeWashed;
    private float timer = 0f;
    public float timerInterval = 10f;

    public PetUiController petUiController;

    

    public void Initialize(int satiety, int happiness, int cleannes,
    int satietyTickRate, int happinessTickRate, int cleannesTickRate)
    {
        this.satiety   = Mathf.Clamp(satiety   - satietyTickRate   * TickAmountToCurrentTime(lastTimeFed, hourLength), 0, 100);
        this.happiness = Mathf.Clamp(happiness - happinessTickRate * TickAmountToCurrentTime(lastTimeHappy, hourLength), 0, 100);
        this.cleannes  = Mathf.Clamp(cleannes  - cleannesTickRate  * TickAmountToCurrentTime(lastTimeWashed, hourLength), 0, 100);
        this.satietyTickRate = satietyTickRate;
        this.happinessTickRate = happinessTickRate;
        this.cleannesTickRate = cleannesTickRate;

        lastTimeFed = DateTime.Now;
        lastTimeHappy = DateTime.Now;
        lastTimeWashed = DateTime.Now;
        GoodParentCheck();
        RefreshScales();

        
        
    }

    public void LoadFromData(BaesData data)
    {
    
        lastTimeFed    = DateTime.Parse(data.lastTimeFed);
        lastTimeHappy  = DateTime.Parse(data.lastTimeHappy);
        lastTimeWashed = DateTime.Parse(data.lastTimeWashed);

        satiety    = data.satiety;
        happiness  = data.happiness;
        cleannes   = data.cleannes;
        GoodParentCheck();
        RefreshScales();
       
    }
    
    void RefreshScales()
    {
        if (petUiController != null)
            petUiController.UpdateScales(happiness, cleannes, satiety);
    }

    private bool isDead = false;

    public void DieOnce()
    {
        if (isDead) return;

            isDead = true;
            Debug.LogFormat("Твой питомец умер (っ °Д °;)っ");
            //if (baesAnimator != null)
                //baesAnimator.SetTrigger("Die"); // если есть анимация смерти
    
    }


    public void ChangeSatiety(int amount)
    {
        satiety = Mathf.Clamp(satiety + amount, 0, 100);
        if (amount > 0) lastTimeFed = DateTime.Now;
        RefreshScales();
    }

    public void ChangeHappiness(int amount)
    {
        happiness = Mathf.Clamp(happiness + amount, 0, 100);
        if (amount > 0) lastTimeHappy = DateTime.Now;
        RefreshScales();
    }

    public void ChangeCleannes(int amount)
    {
        cleannes = Mathf.Clamp(cleannes + amount, 0, 100);
        if (amount > 0) lastTimeWashed = DateTime.Now;
        RefreshScales();
    }

    private bool isSad;
    public void GoodParentCheck()
    {
        isSad = happiness <= 60 || cleannes <= 40 || satiety <= 50;
        baehappy.SetActive(!isSad);
        baesad.SetActive(isSad);
    }
    
    public  float gameHourTimer;
    public  float hourLength;

    void Update()
    {
        GoodParentCheck();
        timer += Time.deltaTime;
        if(timer >= timerInterval)
        {
            timer = 0f;
            ChangeSatiety(-satietyTickRate);
            ChangeHappiness(-happinessTickRate);
            ChangeCleannes(-cleannesTickRate);
            GoodParentCheck();

            if(gameHourTimer <= 0) gameHourTimer = hourLength;
            else gameHourTimer -= Time. deltaTime;
        }

        if (satiety <= 10 || happiness <= 10 || cleannes <= 10)
        DieOnce();
    }

       public int TickAmountToCurrentTime(DateTime lastTime, float tickRateInSeconds)
   {
       double seconds = (DateTime.Now - lastTime).TotalSeconds;
       return Mathf.Min(1500, Mathf.RoundToInt((float)(seconds / tickRateInSeconds)));
   }

    
}
