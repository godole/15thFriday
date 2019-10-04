using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;

public class Ghost : CharacterBase
{
    public GameObject m_EnergyBalls;
    public GameObject m_EnergyBallEaten;

    public GameSceneManager m_GameScene;

    private int m_EnergyBallCount = 0;

    PhotonView m_PhotonView;

    bool m_IsStunned = false;
    bool m_IsRevivaled = false;

    List<EnergyBall> m_EatenEnergyBalls = new List<EnergyBall>();

    public int EnergyBallCount { get => m_EnergyBallCount; set => m_EnergyBallCount = value; }

    protected override void Init()
    {
        base.Init();

        m_PhotonView = GetComponent<PhotonView>();

        m_GameScene = FindObjectOfType<GameSceneManager>();
    }

    public void Hit()
    {
        if (m_IsStunned)
            return;

        RealHit();
    }

    [PunRPC]
    void RealHit()
    {
        ResetEnergyBall();
        ReturnEnergyBall();
        StartCoroutine(Stunned());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && m_PhotonView.IsMine)
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

        base.Move(horizontal, vertical);
    }

    public void EnterRevival()
    {
        m_IsRevivaled = true;
    }

    [PunRPC]
    public void EatEnergyBall()
    {
        var obj = Physics.OverlapSphere(transform.position, 8.74f, 1 << 9);

        if (obj.Length == 0)
            return;

        EnergyBall energyball = null;

        foreach(var e in obj)
        {
            var _e = e.GetComponent<EnergyBall>();
            if (_e.IsActive)
                energyball = _e;
        }

        if (energyball == null)
            return;

        energyball.Eaten();
        m_EatenEnergyBalls.Add(energyball);

        m_EnergyBallCount++;

        CreateEnergyEff();
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

    void ReturnEnergyBall()
    {
        foreach(var e in m_EatenEnergyBalls)
        {
            e.Return();
        }
    }

    void CreateEnergyEff()
    {
        Vector3 position = m_EnergyBalls.transform.position + new Vector3(0, 1.5f + m_EnergyBallCount, 0);
        var eaten = Instantiate(m_EnergyBallEaten);
        eaten.transform.parent = m_EnergyBalls.transform;
        eaten.transform.position = position;
        eaten.transform.rotation = new Quaternion();
    }
}
