using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    void Start()
    {
        GameEvents.Instance.OnBoatDestroyed += BoatExplosion;
        GameEvents.Instance.OnFireCannon += FireExplosion;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnBoatDestroyed -= BoatExplosion;
        GameEvents.Instance.OnFireCannon -= FireExplosion;
    }

    private void BoatExplosion(Vector3 position)
    {
        PoolingManager.Instance.TakeFromPool(PoolObject.BOAT_EXPLOSION, position);
    }

    private void FireExplosion(Vector3 position)
    {
        PoolingManager.Instance.TakeFromPool(PoolObject.FIRE_EXPLOSION, position);
    }
}
