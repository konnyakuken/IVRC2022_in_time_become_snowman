using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawHand : MonoBehaviour
{
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
        RaycastHit hit;
        Ray ray = new Ray(R_ctrlPos.transform.position, R_ctrlPos.transform.forward);

        Debug.Log(R_ctrlPos.gameObject.transform.position);
        
        // ���[�U�[�̋N�_
        line.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // レーザーの終点（オブジェクトにぶつかった場合）
            line.SetPosition(1, hit.point);

            //照射先のオブジェクト（コライダー）
            GameObject target = hit.collider.gameObject;

            if (Input.GetKey(KeyCode.A))
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

                    //ここに終了処理を入れる（仮）
                    this.gameObject.SetActive(false);
                    //
                }
            }

        }
        else
        {
            // レーザーの終点（何にもぶつからなかった場合）
            line.SetPosition(1, ray.origin + (ray.direction * maxDistance));
        }
    }
}
