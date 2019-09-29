using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;

public class Ghost : CharacterBase
{
    public float m_TurnSpeed;
    public GameObject m_EnergyBalls;
    public GameObject m_EnergyBallEaten;

    public GameSceneManager m_GameScene;

    GameObject m_RangedEnergyBall;
    private int m_EnergyBallCount = 0;

    PhotonView m_PhotonView;

    bool m_IsStunned = false;
    bool m_IsRevivaled = false;

    public int EnergyBallCount { get => m_EnergyBallCount; set => m_EnergyBallCount = value; }

    protected override void Init()
    {
        base.Init();

        m_PhotonView = GetComponent<PhotonView>();

        m_GameScene = FindObjectOfType<GameSceneManager>();
    }

    public void Hit()
    {
        Debug.Log("Hit");
        RealHit();
    }

    [PunRPC]
    void RealHit()
    {
        if (m_IsStunned)
            return;

        ResetEnergyBall();
        StartCoroutine(Stunned());
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            m_PhotonView.RPC("EatEnergyBall", RpcTarget.AllViaServer);
        }
    }

    IEnumerator Stunned()
    {
        m_IsStunned = true;
        yield return new WaitForSeconds(10.0f);
        m_IsStunned = false;
    }

    public override void Attack()
    {
        
    }

    public override void Move(float horizontal, float vertical)
    {
        if (m_IsRevivaled)
            return;

        if (m_IsStunned)
            return;

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

    public void EnterRevival()
    {
        m_IsRevivaled = true;
    }

    [PunRPC]
    public void EatEnergyBall()
    {
        if(m_RangedEnergyBall != null)
        {
            Vector3 position = m_EnergyBalls.transform.position + new Vector3(0, 1.5f + m_EnergyBallCount, 0);
            var eaten = Instantiate(m_EnergyBallEaten);
            eaten.transform.parent = m_EnergyBalls.transform;
            eaten.transform.position = position;
            eaten.transform.rotation = new Quaternion();
            Destroy(m_RangedEnergyBall);
            m_EnergyBallCount++;
        }
    }

    [PunRPC]
    public void EnterGate()
    {
        m_GameScene.AddEnergy(m_EnergyBallCount);
        m_PhotonView.RPC("ResetEnergyBall", RpcTarget.AllViaServer);
    }

    [PunRPC]
    void ResetEnergyBall()
    {
        m_EnergyBallCount = 0;
        Destroy(m_EnergyBalls);

        m_EnergyBalls = new GameObject("EnergyBalls");
        m_EnergyBalls.transform.parent = gameObject.transform;
        m_EnergyBalls.transform.localPosition = Vector3.zero;
        m_EnergyBalls.transform.localRotation = new Quaternion();
    }
}
