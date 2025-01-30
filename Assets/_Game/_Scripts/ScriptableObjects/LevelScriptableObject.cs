using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData")]
public class LevelScriptableObject : ScriptableObject
{
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private int waveGenerationAmount;
    [SerializeField] private float waveGenerationDelay;
    [SerializeField] private int perEnemyHealth;
    [SerializeField] private float levelDuration;
    public GameObject LevelPrefab => levelPrefab;
    public int WaveGenerationAmount => waveGenerationAmount;
    public float WaveGenerationDelay => waveGenerationDelay;
    public float LevelDuration => levelDuration;
    public int PerEnemyHealth => perEnemyHealth;
}
