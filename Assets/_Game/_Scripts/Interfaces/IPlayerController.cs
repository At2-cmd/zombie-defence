using UnityEngine;
public interface IPlayerController
{
    void DealDamageToPlayer(float damageAmount);
    void SetPlayableStatusOfPlayer(bool status);
    void UpdateModelLookRotation(Vector3 targetPos);
}
