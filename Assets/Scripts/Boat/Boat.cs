using UnityEngine;

public enum BoatType
{
    PLAYER,
    SHOOTER,
    CHASER,
}

public abstract class Boat : MonoBehaviour
{
    [SerializeField] private BoatType m_boatType;
    [SerializeField] protected Health m_health;
    [SerializeField] protected BoatMovement m_movement;
    [SerializeField] protected Attack m_attack;
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    [SerializeField] protected int m_crashDamage = 3;

    protected bool m_isActive = true;

    private void OnEnable()
    {
        GameEvents.Instance.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnGameOver -= OnGameOver;
    }

    public virtual void TakeDamage(int damage)
    {
        m_health.TakeDamage(damage);
        ChangeSprite();
    }

    public void Reset()
    {
        m_isActive = true;
        m_health.Reset();
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if (!m_health.NeedChangeSprite()) return;
        m_spriteRenderer.sprite = BoatSprites.Instance.BoatSpriteList[m_boatType][m_health.GetState()];
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Boat")) return;

        Boat boat = col.gameObject.GetComponent<Boat>();
        boat.TakeDamage(m_crashDamage);
        if (m_boatType == BoatType.CHASER)
        {
            TakeDamage(100);
        }
    }

    private void OnGameOver()
    {
        m_isActive = false;
    }
}
