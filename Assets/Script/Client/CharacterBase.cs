using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterBase : MonoBehaviour
{
    public float m_WalkSpeed;
    public float m_TurnSpeed;

    protected MouseLook m_MouseLook;
    protected Camera m_Camera;
    protected CharacterController m_CharacterController;
    protected Vector3 m_MoveDir = Vector3.zero;

    void Start()
    {
        m_MouseLook = new MouseLook();
        m_Camera = Camera.main;
        m_CharacterController = GetComponent<CharacterController>();
        m_MouseLook.Init(transform, m_Camera.transform);
    }

    public abstract void Move(float horizontal, float vertical);

    public abstract void Attack();
}
