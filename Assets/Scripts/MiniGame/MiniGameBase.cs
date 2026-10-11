using System;
using UnityEngine;

[DisallowMultipleComponent]
public class MiniGameBase : MonoBehaviour
{
    public float timer = 30f;
    public Transform[] startingPositions;

    public event Action<int> ScoreChanged;
    public event Action<float> TimerChanged;
    public event Action<int, float> Finished;
    public event Action<int, float> Failed;

    protected int score;
    private bool isFinished;

    protected virtual void Start()
    {
        if (timer <= 0f) timer = 30f;
        ScoreChanged?.Invoke(score);
        TimerChanged?.Invoke(timer);
    }

    
    public Vector2 GetStartPosition()
    {
        if (startingPositions == null || startingPositions.Length == 0)
        {
            Debug.LogWarning("У уровня нет startingPositions", this);
            return transform.position;
        }
        return startingPositions[UnityEngine.Random.Range(0, startingPositions.Length)].position;
    }

    public virtual void ChangeScore(int amount)
    {
        if (isFinished) return;
        score += amount;
        ScoreChanged?.Invoke(score);
    }

    public virtual void ChangeTimer(float change)
    {
        timer = Mathf.Max(0f, timer + change);
        TimerChanged?.Invoke(timer);
    }

    protected virtual void Update()
    {
        if (isFinished) return;

        ChangeTimer(-Time.deltaTime);
        if (timer <= 0f) Finish();
    }

    protected void Finish()
    {
        if (isFinished) return;
        isFinished = true;
        Finished?.Invoke(score, timer);
        
    }

    public void Fail()
    {
        if (isFinished) return;
        isFinished = true;           // Update перестанет тикать таймер, ChangeScore перестанет работать
        Failed?.Invoke(score, timer);
    }

    protected virtual void GoalReached()
    {
        Finish();
    }
}