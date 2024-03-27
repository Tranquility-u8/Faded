using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour, IInteractive
{
    private bool isOpen = false;
    private bool isOnDoor = false;

    [SerializeField] private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //test
        OnInteract();
    }

    public void openDoor()
    {
        animator.Play("open");
        AudioManager.instance.inOpenDoor();
        isOpen = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOnDoor = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnDoor = false;
    }

    public void OnInteract()
    {
        if (isOpen && isOnDoor && Input.GetKeyDown(KeyCode.Return))
        {
            DungeonManager.instance.downStair();
        }
    }
}
