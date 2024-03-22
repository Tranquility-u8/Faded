using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    
    public Vector3 position;
    public Rigidbody2D rb;
    public Animator animator;

    public abstract void OnPickUp(Player player);

}
