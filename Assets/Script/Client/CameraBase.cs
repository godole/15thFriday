using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour
{
    public float m_MaxY;
    public float m_MinY;

    public float m_SensivityX = 4.0f;
    public float m_SensivityY = 1.0f;

    protected float currentX = 0.0f;
    protected float currentY = 0.0f;

    protected Camera m_Cam;

    // Start is called before the first frame update
    void Start()
    {
        m_Cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotation(float xAxis, float yAxis)
    {
        currentX += xAxis;
        currentY += yAxis;
    }
}
