using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : CameraBase
{
    public Transform m_Targer;
    public Vector3 m_DeltaPos;

    private void Update()
    {
        transform.position = m_Targer.position + m_DeltaPos;
    }

    void LateUpdate()
    {
        Quaternion rot = Quaternion.Euler(currentY, currentX, 0);
        transform.rotation = rot;
    }
}
