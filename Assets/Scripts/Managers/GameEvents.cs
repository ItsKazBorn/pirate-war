using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public event Action<float, float> OnGameStart;
    public void GameStart(float gameTime, float enemySpawnTime)
    {
        OnGameStart?.Invoke(gameTime, enemySpawnTime);
    }
    
    public event Action OnGameOver;
    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public event Action<Vector3> OnBoatDestroyed;
    public void BoatDestroyed(Vector3 position)
    {
        OnBoatDestroyed?.Invoke(position);
    }

    public event Action<Vector3> OnFireCannon;
    public void FireCannon(Vector3 position)
    {
        OnFireCannon?.Invoke(position);
    }
}
