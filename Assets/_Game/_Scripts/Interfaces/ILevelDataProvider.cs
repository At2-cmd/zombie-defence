using UnityEngine;

public interface ILevelDataProvider
{
    int WaveGenerationAmountForLevel { get; }
    float WaveGenerationDelayForLevel { get; }
    float LevelDuration { get; }
}
