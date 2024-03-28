using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSpike : SpikeBase
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isActivated = true;
    }
}
