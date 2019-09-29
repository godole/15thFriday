using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class UserInput : MonoBehaviour
{
    public CharacterBase m_Client;
    public CameraBase m_Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            m_Client.Attack();
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float mousex = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");

        m_Client.Move(horizontal, vertical);
        m_Camera.Rotation(mousex, mousey);
    }
}
