public class Chaser : Enemy
{
    protected override void AttackPlayer()
    {
        m_movement.MoveForward();
    }
}
