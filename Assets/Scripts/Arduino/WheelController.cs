using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : BaseArduinoController
{

    //時間の経過時間を計測するタイミングを何秒開けるかを指定してあげる
    [SerializeField] float resetSpeedTime = 0.5f;
    //ポート先から取得したローテーションの回転数を代入する変数
    float inputRotateCount = 0;
    //inputRotateCountを一時的に保存しておく為の一時的な変数
    float previousRotateCount = 0;
    // スピードを出力する先
    [NonSerialized] public float speed = 0;
    
    void Start()
    {
        PortOpen();
        // スピードを計算する為の処理を行うこルーチンSpeedCheckerをresetSpeedTime毎に実行する
        StartCoroutine(SpeedChecker(resetSpeedTime));
    }

    void Update()
    {
        //もしシリアルポートが接続されていたら
        if (serialPort_.IsOpen)
        {
            //シリアルポートで出力されている値をUnityで使えるように取得する
            float.TryParse(serialPort_.ReadLine(), out inputRotateCount);
        }
    }
    
    IEnumerator SpeedChecker(float waitTime = 0.1f)
    {
        //無限にループさせる
        while (true)
        {
            //スピードを求める
            //inputRotateCount(今回の回転数) - previousRotateCount(前回の回転数)で回転した距離を求める
            //回転数を速度を計算するまでの待機時間(waitTime)で割ることで、速度を求める。
            speed = ((inputRotateCount - previousRotateCount) / waitTime) / 70;
            //今の回転数を次回に計算できるようにする為にpreviousRotateCountへ保存する
            previousRotateCount = inputRotateCount;
            // waitTimeで指定した秒数ループします
            Debug.Log(speed);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
