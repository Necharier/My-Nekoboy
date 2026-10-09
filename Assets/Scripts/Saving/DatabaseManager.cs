using UnityEngine;
using System;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;
    private Database database;
    private float timer = 0f;
    public float saveInterval = 5f;

    public NeedsController needsController;

    private void Awake()
    {
        database = new Database();
        if (instance == null)
            instance = this;
        else
            Debug.LogWarning("Больше одного DatabaseManager в сцене");
    }

    void Start()
    {
        BaesData data = LoadBaes();
        if (data != null && needsController != null)
        {
            needsController.LoadFromData(data);
        }
    }

    public void SaveBaes()
    {
        if (needsController == null) return;

        BaesData data = new BaesData
        {
            
            lastTimeFed    = needsController.lastTimeFed.ToString("o"),
            lastTimeHappy  = needsController.lastTimeHappy.ToString("o"),
            lastTimeWashed = needsController.lastTimeWashed.ToString("o"),

            
            satiety    = needsController.satiety,
            happiness  = needsController.happiness,
            cleannes   = needsController.cleannes
        };

        database.SaveData("baes", data);
    }

    public BaesData LoadBaes()
    {
        BaesData returnValue = null;
        database.LoadData<BaesData>("baes", (data) =>
        {
            returnValue = data;
        });
        return returnValue;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= saveInterval)
        {
            timer = 0f;
            SaveBaes();
        }
    }

    private void OnApplicationQuit()
    {
        SaveBaes();
    }
}