using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 雪だるまのサイズと回転を操作するスクリプト
/// </summary>
/// 
public class SnowBallStatus : MonoBehaviour
{
    public float Speed;//回転速度
    public float FieldSpeed;//地面が動く速度
    public float Size;//雪玉サイズ

    public GameObject Field;

    // Start is called before the first frame update
    void Start()
    {
        Speed = 0;
        FieldSpeed = 0;
        Size = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Size += Speed * 0.001f;
        FieldSpeed += Speed *0.02f;
        this.gameObject.transform.Rotate(new Vector3(Speed, 0, 0));
        this.gameObject.transform.localScale = new Vector3(Size, Size, Size);

        //雪玉が地面に埋まらないようにするため
        this.gameObject.transform.position = new Vector3(0, 0+Size/2, -7);

        //ステージを動かす
        Field.transform.position = new Vector3(-500, 0, -500-FieldSpeed);
    }
}
