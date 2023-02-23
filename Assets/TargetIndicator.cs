using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] private float m_hideDistance = 10;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Sprite m_chaser;
    [SerializeField] private Sprite m_shooter;
    
    private Transform m_target;
    private Transform m_player;

    public void Setup(Transform target, PoolObject boatType)
    {
        m_player = GameManager.Instance.Player.transform;
        transform.position = m_player.position;
        m_target = target;

        switch (boatType)
        {
            case PoolObject.CHASER:
                m_spriteRenderer.sprite = m_chaser;
                break;
            case PoolObject.SHOOTER:
                m_spriteRenderer.sprite = m_shooter;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_player.position;
        
        Vector3 dir = m_target.position - transform.position;

        SetChildrenActive(!(dir.magnitude < m_hideDistance));

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
    
    
}
