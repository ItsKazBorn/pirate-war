using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private PoolingManager m_bulletPooler;
    
    [SerializeField] private float m_timeToReload = 1;
    private float m_reloadingSides = 0;
    private bool m_canAttackSides = true;
    private float m_reloadingFront = 0;
    private bool m_canAttackFront = true;

    #region Bullet Spawn Positions
    private readonly Vector3 m_frontPos = new Vector3(0, 0.6f, 0);

    private readonly List<Vector3> m_leftPos = new List<Vector3>()
    {
        new Vector3(0.4f, 0, 0),
        new Vector3(0.4f, 0.25f, 0),
        new Vector3(0.4f, -0.25f, 0)
    };
    
    private readonly List<Vector3> m_rightPos = new List<Vector3>()
    {
        new Vector3(-0.4f, 0, 0),
        new Vector3(-0.4f, 0.25f, 0),
        new Vector3(-0.4f, -0.25f, 0)
    };

    private readonly Vector3 m_leftRot = new Vector3(0, 0, -90);
    private readonly Vector3 m_rightRot = new Vector3(0, 0, 90);
    #endregion

    private void Update()
    {
        ReloadSidesTimer();
        
        ReloadFrontTimer();
    }
    
    #region Attacks
    public void ShootForward()
    {
        if (!m_canAttackFront) return;
        SpawnBullet(m_frontPos);
        m_reloadingFront = m_timeToReload;
        m_canAttackFront = false;
    }

    public void ShootSides() // Not currently used, but left here for convenience of future use
    {
        if (!m_canAttackSides) return;
        foreach (Vector3 pos in m_leftPos)
        {
            GameObject ball = SpawnBullet(pos);
            ball.transform.Rotate(m_leftRot);
        }

        foreach (Vector3 pos in m_rightPos)
        {
            GameObject ball = SpawnBullet(pos);
            ball.transform.Rotate(m_rightRot);
        }
        m_reloadingSides = m_timeToReload;
        m_canAttackSides = false;
    }

    public void ShootLeft()
    {
        if (!m_canAttackSides) return;
        foreach (Vector3 pos in m_leftPos)
        {
            GameObject ball = SpawnBullet(pos);
            ball.transform.Rotate(m_leftRot);
        }
        m_reloadingSides = m_timeToReload;
        m_canAttackSides = false;
    }

    public void ShootRight()
    {
        if (!m_canAttackSides) return;
        foreach (Vector3 pos in m_rightPos)
        {
            GameObject ball = SpawnBullet(pos);
            ball.transform.Rotate(m_rightRot);
        }
        m_reloadingSides = m_timeToReload;
        m_canAttackSides = false;
    }
    #endregion

    #region Reload
    private void ReloadSidesTimer()
    {
        if (m_canAttackSides) return;
        if (m_reloadingSides > 0) m_reloadingSides -= Time.deltaTime;
        else m_canAttackSides = true;
    }

    private void ReloadFrontTimer()
    {
        if (m_canAttackFront) return;
        if (m_reloadingFront > 0) m_reloadingFront -= Time.deltaTime;
        else m_canAttackFront = true;
    }
    #endregion
    
    private GameObject SpawnBullet(Vector3 pos)
    {
        Vector3 ballPos = transform.position + transform.TransformDirection(pos);
        GameObject ball = PoolingManager.Instance.TakeFromPool(PoolObject.CANNON_BALL, ballPos);
        ball.transform.rotation = transform.rotation;
        GameEvents.Instance.FireCannon(ballPos);
        return ball;
    }

    
}

