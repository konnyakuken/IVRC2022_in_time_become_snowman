using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DrawVIVE : MonoBehaviour
{
    private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    //���ʂ̊i�[�pBoolean�^�֐�interacrtui
    private bool RightControllerOn;

    public Transform anchor;
    private float maxDistance = 100;
    private LineRenderer line;
    private bool isRotating = false;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    //1�t���[�����ɌĂяo�����Update���]�b�g
    void Update()
    {
        //���ʂ�GetState�Ŏ擾����interacrtui�Ɋi�[
        //SteamVR_Input_Sources.�@�햼
        RightControllerOn = Iui.GetState(SteamVR_Input_Sources.RightHand);

        RaycastHit hit;
        Ray ray = new Ray(anchor.position, anchor.forward);

        // ���[�U�[�̋N�_
        line.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // ���[�U�[�̏I�_�i�I�u�W�F�N�g�ɂԂ������ꍇ�j
            line.SetPosition(1, hit.point);

            // �ω��i��]�j
            GameObject target = hit.collider.gameObject;

            if (RightControllerOn)
            {
                if (target.CompareTag("SnowBall"))
                {
                    Debug.Log("hit");
                }
            }
        }
        else
        {
            // ���[�U�[�̏I�_�i���ɂ��Ԃ���Ȃ������ꍇ�j
            line.SetPosition(1, ray.origin + (ray.direction * maxDistance));
        }
    }
}