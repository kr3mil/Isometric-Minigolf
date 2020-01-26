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
    private int m_TotalShotCount;
    private Vector3 m_MousePoint;
    private float m_IsShootingDelay = 2f;
    private float m_IsShootingCounter;
    private bool m_CollisionsEnabled;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        m_LineRenderer = GetComponent<LineRenderer>();
        m_LineRenderer.positionCount = 2;
        m_RigidBody = GetComponent<Rigidbody>();
        SetCollisions(false);
    }

    void Update()
    {
        if (GameState.IsShooting)
        {
            GetMousePos();

            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Shoot());
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject.Find("LevelManager").GetComponent<LevelManager>().ResetHole(gameObject);
        }
    }

    private void LateUpdate()
    {
        if(m_RigidBody.velocity.sqrMagnitude == 0)
        {
            m_IsShootingDelay -= Time.fixedDeltaTime;
            if(m_IsShootingDelay <= 0)
            {
                GameState.IsShooting = true;
            }
        }
        else
        {
            m_IsShootingCounter = m_IsShootingDelay;
            GameState.IsShooting = false;
        }
    }

    public void MoveToNextHole(Transform positionOfNext, bool resetShots)
    {
        m_RigidBody.isKinematic = true;
        transform.position = positionOfNext.position;
        m_RigidBody.isKinematic = false;
        m_RigidBody.rotation = Quaternion.identity;
        if (resetShots)
        {
            m_TotalShotCount += m_ShotCount;
            m_ShotCount = 0;
        }
        SetCollisions(true);
    }

    public void SetCollisions(bool val)
    {
        var col = GetComponent<Collider>();
        foreach(var otherPlayer in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(otherPlayer != gameObject)
            {
                Physics.IgnoreCollision(col, otherPlayer.GetComponent<Collider>(), val);
            }
        }
        m_CollisionsEnabled = val;
    }

    private void GetMousePos()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
        float rayLength;
        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            m_MousePoint = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(transform.position, m_MousePoint, Color.blue);
            var dir = (m_MousePoint - transform.position).normalized;
            m_ShootDirection = dir;
            m_LineRenderer.enabled = true;
            m_LineRenderer.SetPosition(0, transform.position);
            m_LineRenderer.SetPosition(1, transform.position + dir * m_ShotPower);
        }
    }

    private IEnumerator Shoot()
    {
        m_RigidBody.rotation = Quaternion.identity;
        var force = new Vector3(m_ShootDirection.x, 0, m_ShootDirection.z) * m_ShotPower * 5;
        m_RigidBody.AddForce(force, ForceMode.Impulse);
        m_ShotCount++;
        if (!m_CollisionsEnabled)
        {
            yield return new WaitForSeconds(.3f);
            SetCollisions(true);
        }
    }
}
