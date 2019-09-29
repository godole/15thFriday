using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;

public class Reaper : CharacterBase
{
    public PhotonView m_PhotonView;
    public Vector3 m_AttackCenter;
    //public Vector3 m_AttackSize;
    public float m_AttackSize;


    protected override void Init()
    {
        base.Init();

        m_PhotonView = GetComponent<PhotonView>();
    }
    public override void Attack()
    {
        m_PhotonView.RPC("RealAttack", RpcTarget.AllViaServer);
    }

    public override void Move(float horizontal, float vertical)
    {
        CameraBasedMove(horizontal, vertical);
    }

    [PunRPC]
    void RealAttack()
    {
        var obj = Physics.OverlapSphere(m_AttackCenter + transform.position, m_AttackSize, 1 << 10);

        foreach (var o in obj)
        {
            o.gameObject.GetComponent<Ghost>().Hit();
        }
    }
}
