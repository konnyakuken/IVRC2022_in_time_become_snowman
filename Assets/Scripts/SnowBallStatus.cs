using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

/// <summary>
/// ???????????T?C?Y?????]???????????X?N???v?g
/// </summary>
/// 
public class SnowBallStatus : MonoBehaviour
{
    public float Speed;//???]???x
    public float FieldSpeed;//?n???????????x
    public static float Size;//?????T?C?Y

    public GameObject SnowBallTracker;
    public GameObject LeftHandTracker;
    public GameObject SnowFirstPos;
    public GameObject Field;

    public Text message1;
    public Text message2;

    float timer = 0;
    float endtimer = 0;

    public RotateWheel rotateWheel;
    public SerialHandler serialHandler;
    public UpperSerialHandler upperSerialHandler;

    //ポート先から取得したローテーションの回転数を代入する変数
    float inputRotateCount = 0;
    //inputRotateCountを一時的に保存しておく為の一時的な変数
    float previousRotateCount = 0;
    // スピードを出力する先
    public float rotateSpeed = 0;
    //時間の経過時間を計測するタイミングを何秒開けるかを指定してあげる
    [SerializeField] float waitTime = 0.5f;

    private int isJack= 0;

    // Start is called before the first frame update
    void Start()
    {
        FadeController.isFadeIn = true;
        Speed = 0;
        FieldSpeed = 0;
        Size = 1.65f;
        
        //信号を受信したときに、そのメッセージの処理を行う
        serialHandler.OnDataReceived += OnDataReceived;
        upperSerialHandler.serialPort_.Write("d");
    }

    // Update is called once per frame
    void Update()
    {
        //トラッカーの位置に雪玉を持ってくるver
        //this.gameObject.transform.position = SnowBallTracker.transform.position;


        if (Input.GetKeyDown(KeyCode.U))
        {

            upperSerialHandler.serialPort_.Write("u");

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            upperSerialHandler.serialPort_.Write("d");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            upperSerialHandler.serialPort_.Write("s");
        }

        
        if (FieldSpeed <=1){
            upperSerialHandler.serialPort_.Write("d");
        }

        if (FieldSpeed >= 5 && isJack == 0) {
            isJack = 1;
            upperSerialHandler.serialPort_.Write("u");

            DOVirtual.DelayedCall(3.0f, () => {
                upperSerialHandler.serialPort_.Write("d");
            });

        }
        else if (FieldSpeed >= 15 && isJack == 1)
        {
            isJack = 2;
            upperSerialHandler.serialPort_.Write("u");

            DOVirtual.DelayedCall(3.0f, () => {
                upperSerialHandler.serialPort_.Write("d");
            });

        }
        else if (FieldSpeed >= 20 && isJack == 2)
        {
            isJack = 3;
            upperSerialHandler.serialPort_.Write("u");

            DOVirtual.DelayedCall(3.0f, () => {
                upperSerialHandler.serialPort_.Write("d");
            });

        }

        //手の位置から雪玉の位置を決めるver
        if (timer < 10)
        {
            message1.text = "腕を伸ばした状態で、\n正面の雪玉を押せる位置に移動してください。";
            message2.text = "位置調整中...";
            timer += Time.deltaTime;
            SnowFirstPos.transform.position = new Vector3(LeftHandTracker.transform.position.x, LeftHandTracker.transform.position.y - 0.45f, LeftHandTracker.transform.position.z + 0.85f);
        }
        else
        {
            message1.text = "";
            message2.text = "";
            this.gameObject.transform.position = SnowFirstPos.transform.position;

            //speed調整
            
            if (rotateSpeed == 0)
            {
                Speed = 0;
            }
            else
            {
                Speed = rotateSpeed + 2f;
                //message1.text = Speed.ToString();
            }
        }
        
        
        Size += Speed * 0.001f;
        FieldSpeed += Speed *0.02f;
        this.gameObject.transform.Rotate(new Vector3(Speed, 0, 0));
        this.gameObject.transform.localScale = new Vector3(Size, Size, Size);

        //???????n????????????????????????????
        //this.gameObject.transform.position = new Vector3(0, 0+Size/2, -7);

        //?X?e?[?W????????
        Field.transform.position = new Vector3(-500, 0, -500-FieldSpeed);

        if (this.gameObject.transform.localScale.x > 3) End();
    }

    public void End()
    {
        FadeController.isFadeOut = true;
        endtimer += Time.deltaTime;
        if(endtimer > 3) SceneManager.LoadScene("EndScene");
    }

    public void RePosButton()//位置再調整ボタンが押されたとき
    {
        this.gameObject.transform.position = new Vector3(0, 0, 0);
        SnowFirstPos.transform.position = new Vector3(LeftHandTracker.transform.position.x, LeftHandTracker.transform.position.y - 0.45f, LeftHandTracker.transform.position.z + 0.78f);
        timer = 0;
    }


    //受信した信号(message)に対する処理
    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[] { "\n" }, System.StringSplitOptions.None);
        
        try
        {
            inputRotateCount = float.Parse( data[0]);
            //スピードを求める
            //inputRotateCount(今回の回転数) - previousRotateCount(前回の回転数)で回転した距離を求める
            //回転数を速度を計算するまでの待機時間(waitTime)で割ることで、速度を求める。
            rotateSpeed = ((inputRotateCount - previousRotateCount) / waitTime) / 70;
            //今の回転数を次回に計算できるようにする為にpreviousRotateCountへ保存する
            previousRotateCount = inputRotateCount;
            // waitTimeで指定した秒数ループします
            //Debug.Log(rotateSpeed);//Unityのコンソールに受信データを表示
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);//エラーを表示
        }
    }
}
