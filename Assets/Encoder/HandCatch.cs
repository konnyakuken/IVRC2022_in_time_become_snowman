using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// つかみたいオブジェクトにスクリプトをアタッチ
/// </summary>
public class HandCatch : MonoBehaviour
{
    public GameObject R_HandPos;
    public GameObject L_HandPos;

    //CatchBool.csで管理
    public static bool R_catchNow;
    public static bool L_catchNow;
    
    private float ObjScale;
    
    void Update()
    {
        Vector3 posR = R_HandPos.transform.position;
        Vector3 posL = L_HandPos.transform.position;
        float dis = Vector3.Distance(posR,posL);
        ObjScale = this.gameObject.transform.localScale.x;
        Debug.Log("R:"+R_catchNow +" L:" + L_catchNow);
        
        
        if ((dis < ObjScale) && R_catchNow && L_catchNow)
        {
            Debug.Log("Catching");
            //ParentVer　両手が触れた時点で動かせるが、動きがぎこちない
            //transform.SetParent(R_HandPos.transform);
            
            //CenterVer　動きはスムーズだが、球が２点の中心にワープする
            this.transform.position = (R_HandPos.transform.position + L_HandPos.transform.position) / 2;
        }
        else
        {
            this.transform.parent = null;
        }
        
        
    }

    
}
