using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettingsScriptableObject")]
public class GameSettingsScriptableObject : ScriptableObject
{
    public float MinGameTime;
    public float MaxGameTime;

    public float MinEnemySpawnTime;
    public float MaxEnemySpawnTime;
}
