public interface IEnemyController
{
    void StopWaveGeneration();
    void DespawnAllActiveEnemies();
    int KilledEnemyCountInLevel { get; set; }
}
