using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemBase : MonoBehaviour, ICollectible
{

    [SerializeField] protected Animator animator;

    public string itemName;
    public bool isReleased = false;
    public bool isCollected = false;
   
    public abstract void OnCollected();

}
