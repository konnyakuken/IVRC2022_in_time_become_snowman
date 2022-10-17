using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DrawVIVE : MonoBehaviour
{
    private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    //結果の格納用Boolean型関数interacrtui
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

    //1フレーム毎に呼び出されるUpdateメゾット
    void Update()
    {
        //結果をGetStateで取得してinteracrtuiに格納
        //SteamVR_Input_Sources.機器名
        RightControllerOn = Iui.GetState(SteamVR_Input_Sources.RightHand);

        RaycastHit hit;
        Ray ray = new Ray(R_ctrlPos.transform.position, R_ctrlPos.transform.forward);

        // レーザーの起点
        line.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // レーザーの終点（オブジェクトにぶつかった場合）
            line.SetPosition(1, hit.point);

            //照射先のオブジェクト（コライダー）
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
                    Debug.Log("お絵描きを終了します");

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
