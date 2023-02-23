using UnityEngine;

public class Player : Boat
{
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (!m_health.IsAlive()) GameEvents.Instance.GameOver();
    }

    void Update()
    {
        if (!m_health.IsAlive()) return;
        
        // Movement
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        if (vert >= 0.5f)
        {
            // move Forward
            m_movement.MoveForward();
        }
        switch (hori)
        {
            case >= 0.5f:
                // Move side
                m_movement.Rotate(true);
                break;
            case <= -0.5f:
                // Move other side
                m_movement.Rotate(false);
                break;
        }
        
        // Attack
        if (Input.GetButton("Fire1")) m_attack.ShootForward();
        else if (Input.GetButton("Fire2")) m_attack.ShootRight();
        else if(Input.GetButton("Fire3")) m_attack.ShootLeft();
    }
}
