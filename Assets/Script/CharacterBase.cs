using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(CharacterController))]
public class CharacterBase : MonoBehaviour
{
    public float m_WalkSpeed;
    public float m_TurnSpeed;

    private MouseLook m_MouseLook;
    private Camera m_Camera;
    private CharacterController m_CharacterController;
    private Vector3 m_MoveDir = Vector3.zero;

    private float m_TurnAmount = 0.0f;
    private float m_ForwardAmount = 0.0f;
    private float m_MovingTurnSpeed = 360;
    private float m_StationaryTurnSpeed = 180;

    void Start()
    {
        m_MouseLook = new MouseLook();
        m_Camera = Camera.main;
        m_CharacterController = GetComponent<CharacterController>();
        m_MouseLook.Init(transform, m_Camera.transform);
    }

    public void Move(float horizontal, float vertical)
    {
        Vector3 input = new Vector3(horizontal, vertical);
        Vector3 desiredMove = m_Camera.transform.forward * input.y + m_Camera.transform.right * input.x;

        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                           m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        m_MoveDir.x = desiredMove.x * m_WalkSpeed;
        m_MoveDir.z = desiredMove.z * m_WalkSpeed;

        m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

        Rotation(horizontal, vertical);
    }

    void Rotation(float h, float v)
    {
        if(h != 0 || v != 0)
        {
            Vector3 camForward = m_Camera.transform.forward;
            camForward = new Vector3(camForward.x, 0, camForward.z);
            camForward.Normalize();

            float signedangle = Vector3.SignedAngle(Vector3.forward, camForward, Vector3.up);

            Quaternion rotateAngle = Quaternion.Euler(0, signedangle, 0);
            Vector3 targetAngle = rotateAngle * new Vector3(h, 0, v);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetAngle), m_TurnSpeed);
        }
    }

    public virtual void Attack()
    {

    }
}
