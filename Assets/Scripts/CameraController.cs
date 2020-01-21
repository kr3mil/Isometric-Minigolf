using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 m_TargetRotation;
    private float m_FollowSpeed = 2f;
    private GameObject m_LevelParent;
    private GameObject m_Player;
    [SerializeField] private Vector3 m_OtherTarget;
    private bool m_FollowPlayer = true;
    private Camera m_MainCamera;

    private void Start()
    {
        m_MainCamera = Camera.main;
        m_TargetRotation = transform.eulerAngles;
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_OtherTarget = GetCenterOfChildren(GameObject.FindGameObjectWithTag("LevelParent"));
    }
    void Update()
    {
        CheckCameraInput();
        CheckZoom();
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(m_TargetRotation), 60 * Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (m_FollowPlayer)
            FollowTarget(m_Player.transform.position);
        else
            FollowTarget(m_OtherTarget);

        if (Input.GetKeyDown(KeyCode.Q))
            m_FollowPlayer = !m_FollowPlayer;
    }

    private void CheckCameraInput()
    {
        if (transform.rotation == Quaternion.Euler(m_TargetRotation))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                m_TargetRotation.y += 90;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                m_TargetRotation.y -= 90;
            }
        }
    }

    private void CheckZoom()
    {
        if (Input.mouseScrollDelta.y != 0 && Input.GetKey(KeyCode.LeftControl))
        {
            m_MainCamera.orthographicSize -= Input.mouseScrollDelta.y * Time.deltaTime * 15f;
            m_MainCamera.orthographicSize = Mathf.Clamp(m_MainCamera.orthographicSize, .5f, 20f);
        }
    }

    private void FollowTarget(Vector3 pos)
    {
        transform.position = Vector3.Lerp(transform.position, pos, m_FollowSpeed * Time.deltaTime);
    }

    private Vector3 GetCenterOfChildren(GameObject parent)
    {
        var bounds = new Bounds();
        foreach(Transform child in parent.transform)
        {
            bounds.Encapsulate(child.position);
        }
        return bounds.center;
    }
}
