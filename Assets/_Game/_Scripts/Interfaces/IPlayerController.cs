using UnityEngine;
public interface IPlayerController
{
    void DealDamageToPlayer(float damageAmount);
    void SetPlayableStatusOfPlayer(bool status);
}
