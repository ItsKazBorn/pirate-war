using System;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private int m_damage = 1;

    private Vector3 m_dir = new Vector3(0, 1, 0);
    
    void Update()
    {
        transform.Translate(m_dir * (Time.deltaTime * m_speed));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boat"))
        {
            Boat boat = other.gameObject.GetComponent<Boat>();
            boat.TakeDamage(m_damage);
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        gameObject.SetActive(false);
    }
}
