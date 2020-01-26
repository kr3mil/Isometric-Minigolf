using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    private Vector3 m_CurrentPosition;
    private PlayerController m_PlayerController;

    private void Start()
    {
        m_PlayerController = GetComponent<PlayerController>();
    }
    void FixedUpdate()
    {
        if (m_PlayerController.IsShooting)
        {
            m_CurrentPosition = transform.position;
        }
    }

    public void Respawn()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = m_CurrentPosition;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
