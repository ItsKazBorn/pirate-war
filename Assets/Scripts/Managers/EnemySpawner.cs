using UnityEngine;

public class EnemySpawner
{
    private float m_collisionCircleRadius = 1.5f/2f;
    
    public void SpawnEnemy()
    {
        Vector2 pos = GetRandomPosition();
        while (IsOverlapingLand(pos))
        {
            pos = GetRandomPosition();
        }
        PoolObject type = GetRandomEnemyType();
        GameObject enemy = PoolingManager.Instance.TakeFromPool(type, pos);
        TargetIndicator target = PoolingManager.Instance.TakeFromPool(PoolObject.TARGET, enemy.transform.position).GetComponent<TargetIndicator>();
        if (target) target.Setup(enemy.transform, type);
    }

    private Vector2 GetRandomPosition()
    {
        float x = Random.Range(-45, 45);
        float y = Random.Range(-44, 40);
        return new Vector2(x, y);
    }

    private bool IsOverlapingLand(Vector2 position)
    {
        return Physics2D.OverlapCircle(position, m_collisionCircleRadius, LayerMask.GetMask("Land"));
    }

    private PoolObject GetRandomEnemyType()
    {
        int rand = Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                return PoolObject.CHASER;
            case 1:
                return PoolObject.SHOOTER;
            default:
                return PoolObject.SHOOTER;
        }
    }
}
