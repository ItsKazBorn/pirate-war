using TMPro;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_gameTimeText;

    public void SetGameTime(string time)
    {
        m_gameTimeText.text = time;
    }
}
