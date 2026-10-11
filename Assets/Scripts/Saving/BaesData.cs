using System;

[System.Serializable]
public class BaesData
{
    public string lastTimeFed;      // ← было DateTime, стало string
    public string lastTimeHappy;    // ← было DateTime, стало string
    public string lastTimeWashed;   // ← было DateTime, стало string
    public int satiety;
    public int happiness;
    public int cleannes;
}