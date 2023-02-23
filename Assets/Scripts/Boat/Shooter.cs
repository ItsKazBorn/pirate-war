
public class Shooter : Enemy
{
    private float m_angleToAttack = 15f;
    
    protected override void AttackPlayer()
    {
        if (m_angle >= -m_angleToAttack && m_angle <= m_angleToAttack)
        {
            m_attack.ShootForward();
            return;
        }

        float startAngle = m_angleToAttack + 90;
        float endAngle = startAngle - m_angleToAttack * 2;
        if (m_angle <= startAngle && m_angle >= endAngle)
        {
            m_attack.ShootRight();
        }
        if (m_angle >= -startAngle && m_angle <= endAngle)
        {
            m_attack.ShootLeft();
        }
    }
}
