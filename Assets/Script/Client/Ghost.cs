using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Ghost : CharacterBase
{
    public float m_TurnSpeed;

    public void Hit()
    {
        Debug.Log("Hit");
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
}
