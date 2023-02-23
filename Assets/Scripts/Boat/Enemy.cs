using UnityEngine;

public abstract class Enemy : Boat
{
    [SerializeField] protected float m_range = 10;
    
    private Transform m_player;
    
    private Vector3 m_direction;
    protected float m_angle;

    private void Start()
    {
        m_player = GameManager.Instance.Player.transform;
    }

    private void Update()
    {
        if (!m_health.IsAlive()) return;
        if (!m_isActive) return;
        // Check Distance
        if (!CheckIfInRange()) return;
        
        m_angle = GetAngle();
        TurnToPlayer();
        AttackPlayer();
    }

    private bool CheckIfInRange()
    {
        float dist = Vector3.Distance(transform.position, m_player.position);
        return !(dist > m_range);
    }
    
    private float GetAngle()
    {
        m_direction = (m_player.position - transform.position).normalized;
        return Vector2.SignedAngle(transform.up, m_direction);
    }
    
    private void TurnToPlayer()
    {
        if (m_angle <= -5) m_movement.Rotate(true);
        else if (m_angle >= 5) m_movement.Rotate(false);
    }

    protected virtual void AttackPlayer() { }
}
