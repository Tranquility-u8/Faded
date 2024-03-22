using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveItem : ItemBase
{
    int maxCharge;
    int currCharge;

    public abstract void becollected();
    public abstract void beDrop();
}
