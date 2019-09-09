using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterBase : MonoBehaviour
{
    public float m_WalkSpeed;
    private MouseLook m_MouseLook;
    private Camera m_Camera;
    private CharacterController m_CharacterController;
    private Vector3 m_MoveDir = Vector3.zero;

    void Start()
    {
        m_MouseLook = new MouseLook();
        m_Camera = Camera.main;
        m_CharacterController = GetComponent<CharacterController>();
        m_MouseLook.Init(transform, m_Camera.transform);
    }

    // Update is called once per frame
    void Update()
    {
        m_MouseLook.LookRotation(transform, m_Camera.transform);
    }

    private void FixedUpdate()
    {
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            //Collider[] hitcolliders = Physics.
        }

        Vector3 input = new Vector2(horizontal, vertical);
        // always move along the camera forward as it is the direction that it being aimed at
        Vector3 desiredMove = transform.forward * input.y + transform.right * input.x;

        // get a normal for the surface that is being touched to move along it
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                           m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        m_MoveDir.x = desiredMove.x * m_WalkSpeed;
        m_MoveDir.z = desiredMove.z * m_WalkSpeed;

        m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

        m_MouseLook.UpdateCursorLock();
    }
}
