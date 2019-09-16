using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : CharacterBase
{
    public Vector3 m_AttackCenter;
    public Vector3 m_AttackSize;

    public override void Attack()
    {
        var obj = Physics.OverlapBox(m_AttackCenter + transform.position, m_AttackSize / 2, new Quaternion(), 1 << 10);

        foreach(var o in obj)
        {
            o.gameObject.GetComponent<Ghost>().Hit();
        }
    }

    public override void Move(float horizontal, float vertical)
    {
        CameraBasedMove(horizontal, vertical);
    }
}
