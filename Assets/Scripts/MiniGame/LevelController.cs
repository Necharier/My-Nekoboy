using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class LevelController : MonoBehaviour
{
    public Transform bae;
    public MiniGameLevel[] LevelsPrefab;
    public MiniGameLevel firstLevel;
    private List<MiniGameLevel> spawnedLevels = new List<MiniGameLevel>();
    private MiniGameLevel newLevel;
    private void SpawnLevels()
    {
        MiniGameLevel GameLevels = Instantiate(LevelsPrefab[Random.Range(0, LevelsPrefab.Length)]);
        newLevel.transform.position = spawnedLevels[spawnedLevels.Count-1].end.position - newLevel.begin.localPosition;
        spawnedLevels.Add(newLevel);
    }

    private void Start()
    {
        spawnedLevels.Add(firstLevel);
    }
    private void Update()
    {
        if(bae.position.y > spawnedLevels[spawnedLevels.Count - 1].end.position.y)
        {
            SpawnLevels();
        }
    }
}
