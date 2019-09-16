using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : CameraBase
{
    public Transform m_LookAt;
    
    public float distance = 10.0f;
    
    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rot = Quaternion.Euler(currentY, currentX, 0);
        transform.position = m_LookAt.position + rot * dir;
        transform.LookAt(m_LookAt.position);
    }
}
