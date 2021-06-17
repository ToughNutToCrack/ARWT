using UnityEngine;

public class BillboardComponent : MonoBehaviour
{
    public Camera m_Camera;

    private void Awake()
    {
        if(Camera.main != null)
        {
            m_Camera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        if(m_Camera == null)
        {
            return;
        }
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}
