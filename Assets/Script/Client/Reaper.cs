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
}
