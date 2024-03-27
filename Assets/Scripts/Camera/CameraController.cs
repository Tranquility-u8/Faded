using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [Header("Camera")]
    [SerializeField] private float speed;
    private Transform origin;
    private Transform target;

    [Header("Background")]
    public Transform backgroundHolder;
    public Transform background0;
    public Transform background1;
    public Transform background2;
    private List<Transform> backgrounds;
    private int left = 0;
    private int right = 2;
    private float offset = 0;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        origin = this.transform;
        backgrounds = new List<Transform>()
        {
            background0,
            background1,
            background2
        };
    }

    void Update()
    {
        if(target != null)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), speed * Time.deltaTime);
    }

    public void changeTarget(Transform newRoom)
    {
        target = newRoom;
        
        float dx = target.transform.position.x - transform.position.x;
        if (dx > 0)
        {
            backgroundHolder.position = new Vector3(backgroundHolder.position.x - 10, backgroundHolder.position.y, backgroundHolder.position.z);
            offset -= 10;
        }
        else if(dx < 0)
        {
            backgroundHolder.position = new Vector3(backgroundHolder.position.x + 10, backgroundHolder.position.y, backgroundHolder.position.z);
            offset += 10;
        }

        if(offset > 49.5)
        {
            backgrounds[right].position = new Vector3(backgrounds[right].position.x - 99, backgrounds[right].position.y, backgrounds[right].position.z);
            right = (right - 1 + 3) % 3;
            left = (left - 1 + 3) % 3;
            offset = 0;
        }
        else if(offset < -49.5)
        {
            backgrounds[left].position = new Vector3(backgrounds[left].position.x + 99, backgrounds[left].position.y, backgrounds[left].position.z);
            right = (right + 1) % 3;
            left = (left + 1) % 3;
            offset = 0;
        }
    }

    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }

}
