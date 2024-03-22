using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    名称：超视
    ID：0
    描述：显现怪物的本体（黑白灰），
    大幅提高怪物受到的真实伤害
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
