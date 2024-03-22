using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dungeon;

public class Door : MonoBehaviour
{
    public bool isOpen = true;
    public Direction dir;
    public DoorType type = DoorType.NormalDoor;

    private Animator animator;
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Start()
    {


        
    }

    void Update()
    {

    }


    public void setUpDoor()
    { 
        boxCollider2D.enabled = false;
    }

    public void openDoor()
    {
        if(type == DoorType.NormalDoor)
        {
            isOpen = true;
            boxCollider2D.enabled = false;
            StartCoroutine(openDoorAnim());
        }
    }

    public void closeDoor()
    {
        if (type == DoorType.NormalDoor)
        {
            isOpen = false;
            StartCoroutine(closeDoorAnim());
        }

    }

    IEnumerator openDoorAnim()
    {
        yield return new WaitForSeconds(0.5f);
        boxCollider2D.enabled = false;
        animator.Play("door_open");
    }

    IEnumerator closeDoorAnim()
    {
        yield return new WaitForSeconds(0.5f);
        boxCollider2D.enabled = true;
        animator.Play("door_close");
    }


}
