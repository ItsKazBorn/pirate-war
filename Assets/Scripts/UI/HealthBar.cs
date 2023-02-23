using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject m_bar;

    private Vector3 m_scale = new Vector3(1, 1, 1);

    private Vector3 m_offset = new Vector3(0, 1, -1);
    private Transform m_boat;

    public void Setup(Transform boat, int maxLife)
    {
        gameObject.SetActive(true);
        m_boat = boat;
        UpdateBar(maxLife, maxLife);
    }

    public void UpdateBar(float maxLife, float currentLife)
    {
        if (currentLife <= 0) gameObject.SetActive(false);
        
        float size = currentLife / maxLife;

        m_scale.x = size;
        m_bar.transform.localScale = m_scale;
    }

    private void Update()
    {
        if (m_boat) transform.position = m_boat.position + m_offset;
    }
}
