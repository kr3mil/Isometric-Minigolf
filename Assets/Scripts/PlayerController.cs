using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private LineRenderer m_LineRenderer;
    private Vector3 m_ShootDirection;
    private Rigidbody m_RigidBody;
    private float m_ShotPower = 1f;
    private bool m_IsMoving = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        m_LineRenderer = GetComponent<LineRenderer>();
        m_LineRenderer.positionCount = 2;
        m_RigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!m_IsMoving)
        {
            GetMousePos();

            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
            else if (Input.mouseScrollDelta.y != 0)
            {
                m_ShotPower += Input.mouseScrollDelta.y * .1f;
            }
        }
        else
        {
            m_LineRenderer.enabled = false;
            //transform.localEulerAngles = Vector3.zero;
        }
    }

    private void LateUpdate()
    {
        m_IsMoving = m_RigidBody.velocity.magnitude > 0;
    }

    private void GetMousePos()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
        float rayLength;
        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 point = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(transform.position, point, Color.blue);
            var dir = (point - transform.position).normalized;
            m_ShootDirection = dir;
            m_LineRenderer.enabled = true;
            m_LineRenderer.SetPosition(0, transform.position);
            m_LineRenderer.SetPosition(1, transform.position + dir * m_ShotPower);
        }
    }

    private void Shoot()
    {
        m_RigidBody.AddForce(transform.position + m_ShootDirection * m_ShotPower * 5, ForceMode.VelocityChange);
        m_IsMoving = true;
    }
}
