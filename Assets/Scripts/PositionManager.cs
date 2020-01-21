using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    private Vector3 m_CurrentPosition;
    void FixedUpdate()
    {
        if (GameState.IsShooting)
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
