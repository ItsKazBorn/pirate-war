using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rigidBody;
    [SerializeField] private float m_moveSpd = 100;
    [SerializeField] private float m_rotateSpd = 100;

    public void MoveForward()
    {
        m_rigidBody.AddRelativeForce(Vector2.up * (m_moveSpd * Time.deltaTime));
    }

    public void Rotate(bool left)
    {
        if (left)
        {
            RotateLeft();
        }
        else
        {
            RotateRight();
        }
    }

    private void RotateLeft()
    {
        m_rigidBody.MoveRotation(m_rigidBody.rotation - m_rotateSpd * Time.deltaTime);
    }

    private void RotateRight()
    {
        m_rigidBody.MoveRotation(m_rigidBody.rotation + m_rotateSpd * Time.deltaTime);
    }
}
