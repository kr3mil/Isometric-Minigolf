using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private LineRenderer m_LineRenderer;
    private Vector3 m_ShootDirection;
    private Rigidbody m_RigidBody;
    [SerializeField] private float m_ShotPower = 1f;
    private int m_ShotCount;
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
        if (GameState.IsShooting)
        {
            GetMousePos();

            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
            else if (Input.mouseScrollDelta.y != 0 && !Input.GetKey(KeyCode.LeftControl))
            {
                m_ShotPower = Mathf.Clamp(m_ShotPower += Input.mouseScrollDelta.y * .1f, .1f, 5f);
            }
        }
        else
        {
            m_LineRenderer.enabled = false;
        }
    }

    private void LateUpdate()
    {
        GameState.IsShooting = m_RigidBody.velocity.sqrMagnitude == 0;
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
        m_RigidBody.rotation = Quaternion.identity;
        m_RigidBody.AddForce(transform.position + m_ShootDirection * m_ShotPower * 5, ForceMode.Impulse);
        m_ShotCount++;
        //GameState.IsShooting = true;
    }
}
