using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DrawVIVE : MonoBehaviour
{
    private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    //���ʂ̊i�[�pBoolean�^�֐�interacrtui
    private bool RightControllerOn;

    public GameObject R_ctrlPos;
    public Transform DrawingObjects;
    public GameObject Ink_Black;
    private float maxDistance = 200;
    private LineRenderer line;
   

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
        Ray ray = new Ray(R_ctrlPos.transform.position, R_ctrlPos.transform.forward);

        // ���[�U�[�̋N�_
        line.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // ���[�U�[�̏I�_�i�I�u�W�F�N�g�ɂԂ������ꍇ�j
            line.SetPosition(1, hit.point);

            //�Ǝː�̃I�u�W�F�N�g�i�R���C�_�[�j
            GameObject target = hit.collider.gameObject;

            if (RightControllerOn)
            {
                if (target.CompareTag("SnowBall"))
                {
                    Debug.Log("Drawing");
                    GameObject Ink_blackCopy = Instantiate(Ink_Black, DrawingObjects) as GameObject;
                    Ink_blackCopy.transform.position = hit.point;
                }
                if (target.CompareTag("DrawFinish"))
                {
                    Debug.Log("���G�`�����I�����܂�");

                    //�����ɏI������������i���j
                    this.gameObject.SetActive(false);
                    //
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
