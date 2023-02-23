using System.Collections;
using UnityEngine;

public class BoatExplosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_particles;

    private void OnEnable()
    {
        m_particles.Play();
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        float time = m_particles.main.duration;
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
