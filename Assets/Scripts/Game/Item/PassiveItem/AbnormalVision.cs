using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    ���ƣ�����
    ID��0
    ���������ֹ���ı��壨�ڰ׻ң���
    �����߹����ܵ�����ʵ�˺�
 */

public class AbnormalVision : PassiveItem
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnCollected()
    {
        throw new System.NotImplementedException();
    }
}
