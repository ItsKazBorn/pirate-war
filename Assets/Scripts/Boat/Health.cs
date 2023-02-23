using UnityEngine;

public enum BoatStates
{
    FINE,
    DAMAGED,
    BREAKING,
    DEAD
}

public class Health : MonoBehaviour
{ 
    [SerializeField] private int m_maxLife = 3;
    [SerializeField] private int m_currentLife;
    [SerializeField] private BoatStates m_state = BoatStates.FINE;

    private HealthBar m_healthBar;

    private void OnEnable()
    {
        SpawnHealthBar();
        m_currentLife = m_maxLife;
    }

    public void Reset()
    {
        m_currentLife = m_maxLife;
        if (m_healthBar) m_healthBar.Setup(transform, m_maxLife);
        else SpawnHealthBar();
    }

    private void OnDisable()
    {
        m_healthBar.gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        m_currentLife -= damage;
        if (m_currentLife <= 0)
        {
            m_currentLife = 0;
            GameEvents.Instance.BoatDestroyed(transform.position);
        }
        m_healthBar.UpdateBar(m_maxLife, m_currentLife);
    }

    private void SpawnHealthBar()
    {
        HealthBar healthBar = PoolingManager.Instance.TakeFromPool(PoolObject.HEALTH_BAR, transform.position).GetComponent<HealthBar>();
        if (healthBar) m_healthBar = healthBar;
        m_healthBar.Setup(transform, m_maxLife);
    }

    public bool NeedChangeSprite()
    {
        BoatStates newState = BoatStates.FINE;
        
        if (m_currentLife <= (m_maxLife / 3 * 2) && m_currentLife > m_maxLife / 3) newState = BoatStates.DAMAGED;
        else if (m_currentLife <= m_maxLife / 3 && m_currentLife > 0) newState = BoatStates.BREAKING;
        else if (m_currentLife <= 0) newState = BoatStates.DEAD;

        if (m_state == newState) return false;
        m_state = newState;
        return true;
    }

    public bool IsAlive()
    {
        return m_currentLife > 0;
    }

    public BoatStates GetState()
    {
        return m_state;
    }
}
