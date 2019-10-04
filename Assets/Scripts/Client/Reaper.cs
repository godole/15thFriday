using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;

public class Reaper : CharacterBase
{
    public PhotonView m_PhotonView;
    ParticleSystem m_AttackEffect;

    public Vector3 m_AttackCenter;
    //public Vector3 m_AttackSize;
    public float m_AttackSize;


    protected override void Init()
    {
        base.Init();

        m_PhotonView = GetComponent<PhotonView>();

        m_AttackEffect = GetComponentInChildren<ParticleSystem>();
    }

    public void DisableMesh()
    {
        var render = GetComponentsInChildren<MeshRenderer>();

        foreach (var r in render)
            r.enabled = false;
    }
    public override void Attack()
    {
        m_PhotonView.RPC("RealAttack", RpcTarget.AllViaServer);
    }

    protected override void Rotation(float h, float v)
    {
        transform.rotation = Quaternion.Euler(0, m_Camera.transform.rotation.eulerAngles.y, 0);
    }

    [PunRPC]
    void RealAttack()
    {
        m_AttackEffect.Play();
        var obj = Physics.OverlapSphere(m_AttackCenter + transform.position, m_AttackSize, 1 << 10);

        foreach (var o in obj)
        {
            o.gameObject.GetComponent<Ghost>().Hit();
        }
    }
}
