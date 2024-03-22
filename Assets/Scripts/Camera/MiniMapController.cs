using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public static MiniMapController instance;
    public GameObject miniMap;

    public float speed;
    public Transform target;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), speed * Time.deltaTime);
    }

    public void changeTarget(Transform newRoom)
    {
        target = newRoom;
    }

}
