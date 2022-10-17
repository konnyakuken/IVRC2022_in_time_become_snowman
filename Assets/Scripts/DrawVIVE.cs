using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DrawVIVE : MonoBehaviour
{
    private SteamVR_Action_Boolean Iui = SteamVR_Actions.default_InteractUI;
    //結果の格納用Boolean型関数interacrtui
    private bool RightControllerOn;

    public Transform anchor;
    private float maxDistance = 100;
    private LineRenderer line;
    private bool isRotating = false;

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
        Ray ray = new Ray(anchor.position, anchor.forward);

        // レーザーの起点
        line.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // レーザーの終点（オブジェクトにぶつかった場合）
            line.SetPosition(1, hit.point);

            // 変化（回転）
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
            // レーザーの終点（何にもぶつからなかった場合）
            line.SetPosition(1, ray.origin + (ray.direction * maxDistance));
        }
    }
}
