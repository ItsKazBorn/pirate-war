using UnityEngine;

public class FollowTarget
{
    private Transform m_target;
    private Transform m_follower;
    private float m_smoothTime = 0.5f;
    
    private Vector3 m_velocity = Vector3.zero;
    private bool m_following = false;

    public void Setup(Transform follower, Transform target, float smoothTime = 0.5f)
    {
        m_follower = follower;
        m_target = target;
        m_smoothTime = smoothTime;
        m_following = true;
    }

    // Update is called once per frame
    public void Follow()
    {
        if (!m_following) return;

        Vector3 targetPos = m_target.position;

        targetPos.z = m_follower.position.z;

        m_follower.position = Vector3.SmoothDamp(m_follower.position, targetPos, ref m_velocity, m_smoothTime);
    }
}
