using System.Collections;
using UnityEngine;

public class FireExplosion : MonoBehaviour
{
    [SerializeField] private Animator m_animator;

    private void OnEnable()
    {
        m_animator.SetTrigger("FireCannon");
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        while (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
        {
            yield return null;
        }
        while (m_animator.GetCurrentAnimatorStateInfo(0).IsName("FireAnimation"))
        {
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
