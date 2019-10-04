using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public abstract class CharacterBase : MonoBehaviour
{
    public float m_TurnSpeed;
    public float m_WalkSpeed;
    
    protected Camera m_Camera;
    protected CharacterController m_CharacterController;
    protected Vector3 m_MoveDir = Vector3.zero;

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        m_Camera = Camera.main;
        m_CharacterController = GetComponent<CharacterController>();
    }

    protected void CameraBasedMove(float horizontal, float vertical)
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
    }

    protected virtual void Rotation(float h, float v)
    {
        if (h != 0 || v != 0)
        {
            Vector3 camForward = m_Camera.transform.forward;
            camForward = new Vector3(camForward.x, 0, camForward.z);
            camForward.Normalize();

            float signedangle = Vector3.SignedAngle(Vector3.forward, camForward, Vector3.up);

            Quaternion rotateAngle = Quaternion.Euler(0, signedangle, 0);
            Vector3 targetAngle = rotateAngle * new Vector3(h, 0, v);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetAngle), m_TurnSpeed * Time.deltaTime);
        }
    }

    public virtual void Move(float horizontal, float vertical)
    {
        CameraBasedMove(horizontal, vertical);

        Rotation(horizontal, vertical);
    }

    public abstract void Attack();
}
