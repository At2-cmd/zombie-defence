using UnityEngine;
using Zenject;

public class PlayerShooter : MonoBehaviour
{
    [Inject] BulletEntity.Pool _bulletEntitiesPool;
    [SerializeField] private LayerMask enemyLayer;
    private Collider[] _detectedEnemiesAround;
    private EnemyEntity _pickedEnemy;
    public void Initialize()
    {
        
    }
    //public void CheckForNearbyEnemy()
    //{
    //    if (_pickedEnemy != null) return;

    //    _detectedEnemiesAround = Physics.OverlapBox(transform.position, Vector3.one * 5, Quaternion.identity,enemyLayer);
    //    if (_detectedEnemiesAround.Length > 0)
    //    {
    //        if (_detectedEnemiesAround[0].TryGetComponent(out EnemyEntity firstEnemyInArray))
    //        {
    //            _pickedEnemy = firstEnemyInArray;
    //        }
    //    }
    //}
}
