using UnityEngine;
using Zenject;

public class EnemyController : MonoBehaviour, IInitializable
{
    [Inject] EnemyEntity.Pool _enemyPool;
    [SerializeField] private int _initialEnemySpawnAmount;

    public void Initialize()
    {
        for (int i = 0; i < _initialEnemySpawnAmount; i++)
        {
            var randomSpawnPos = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            var enemy = _enemyPool.Spawn(randomSpawnPos);
            enemy.SetEnemyInitialHealth(1);
        }
    }
}
