using System;
using System.Collections;
using UnityEngine;
using System.IO.Ports;

public class RotateWheel : MonoBehaviour
{
    //�|�[�g���w�肵�Ă�����
    public string portName = "COM4"; // �|�[�g�ԍ��͎����Ŋm�F���Ă�������
    // baudRate�̓}�C�R����baudRate�ƈ�v������
    const int baudRate = 9600;
    SerialPort serialPort_;
    //���Ԃ̌o�ߎ��Ԃ��v������^�C�~���O�����b�J���邩���w�肵�Ă�����
    [SerializeField] float resetSpeedTime = 0.5f;
    //�|�[�g�悩��擾�������[�e�[�V�����̉�]����������ϐ�
    float inputRotateCount = 0;
    //inputRotateCount���ꎞ�I�ɕۑ����Ă����ׂ̈ꎞ�I�ȕϐ�
    float previousRotateCount = 0;
    // �X�s�[�h���o�͂����
    public float speed = 0;

    private void Start()
    {
        // SerialPort�^�̍\���̂�V�������
        serialPort_ = new SerialPort(portName, baudRate);
        // SerialPort���J��
        serialPort_.Open();
        // �X�s�[�h���v�Z����ׂ̏������s�������[�`��SpeedChecker��resetSpeedTime���Ɏ��s����
        StartCoroutine(SpeedChecker(resetSpeedTime));
    }

    void Update()
    {
        //�����V���A���|�[�g���ڑ�����Ă�����
        if (serialPort_.IsOpen)
        {
            //�V���A���|�[�g�ŏo�͂���Ă���l��Unity�Ŏg����悤�Ɏ擾����
            float.TryParse(serialPort_.ReadLine(), out inputRotateCount);
        }
    }

    private void OnDestroy()
    {
        if (serialPort_ != null && serialPort_.IsOpen)
        {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }

    IEnumerator SpeedChecker(float waitTime = 0.1f)
    {
        //�����Ƀ��[�v������
        while (true)
        {
            //�X�s�[�h�����߂�
            //inputRotateCount(����̉�]��) - previousRotateCount(�O��̉�]��)�ŉ�]�������������߂�
            //��]���𑬓x���v�Z����܂ł̑ҋ@����(waitTime)�Ŋ��邱�ƂŁA���x�����߂�B
            speed = ((inputRotateCount - previousRotateCount) / waitTime) / 70;
            //���̉�]��������Ɍv�Z�ł���悤�ɂ���ׂ�previousRotateCount�֕ۑ�����
            previousRotateCount = inputRotateCount;
            // waitTime�Ŏw�肵���b�����[�v���܂�
            Debug.Log(speed);
            yield return new WaitForSeconds(waitTime);
        }
    }
}