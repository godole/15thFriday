using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Ghost : CharacterBase
{
    public float m_TurnSpeed;
    public GameObject m_EnergyBalls;

    GameObject m_RangedEnergyBall;
    private int m_EnergyBallCount = 0;

    public int EnergyBallCount { get => m_EnergyBallCount; set => m_EnergyBallCount = value; }

    protected override void Init()
    {
        base.Init();
    }

    public void Hit()
    {
        Debug.Log("Hit");
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            EatEnergyBall();
        }
    }

    public override void Attack()
    {
        
    }

    public override void Move(float horizontal, float vertical)
    {
        CameraBasedMove(horizontal, vertical);

        Rotation(horizontal, vertical);
    }

    void Rotation(float h, float v)
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

    public void RangedEnergyBall(GameObject ball)
    {
        m_RangedEnergyBall = ball;
    }

    public void OutOfRangeBall()
    {
        m_RangedEnergyBall = null;
    }

    public void EatEnergyBall()
    {
        if(m_RangedEnergyBall != null)
        {
            m_RangedEnergyBall.transform.parent = m_EnergyBalls.transform;
            m_RangedEnergyBall.transform.position = transform.position + new Vector3(0, 1 + m_EnergyBallCount, 0);
            m_RangedEnergyBall.transform.rotation = new Quaternion();
            m_RangedEnergyBall.GetComponent<EnergyBall>().m_EffectObj.SetActive(false);
            m_EnergyBallCount++;
        }
    }

    public void EnterGate()
    {
        m_EnergyBallCount = 0;
        Destroy(m_EnergyBalls);

        m_EnergyBalls = new GameObject();
        m_EnergyBalls.transform.parent = transform;
        m_EnergyBalls.transform.position = new Vector3();
        m_EnergyBalls.transform.rotation = new Quaternion();
    }
}
