using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private LineRenderer m_LineRenderer;
    private Vector3 m_ShootDirection;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        m_LineRenderer = GetComponent<LineRenderer>();
        m_LineRenderer.positionCount = 2;
    }

    void Update()
    {
        GetMousePos();

        if (Input.GetMouseButtonDown(0))
        {

        }
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
            m_LineRenderer.SetPosition(0, transform.position);
            m_LineRenderer.SetPosition(1, transform.position + dir * 1f);
        }
    }

    private void Shoot()
    {

    }
}
