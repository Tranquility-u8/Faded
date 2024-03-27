using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{

    public GameObject bulletPrefab;
    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    public GameObject Take()
    {
        GameObject instance;
        Player player = GameManager.instance.player;
        if (bulletPool.Count > 0)
        {
            instance = bulletPool.Dequeue();
            instance.SetActive(true);
            instance.transform.SetParent(null);
            instance.transform.position = player.transform.position;
            return instance;
        }
        instance = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        return instance;
    }

    public void Back(GameObject instance)
    {
        bulletPool.Enqueue(instance);
        instance.transform.SetParent(transform);
        instance.SetActive(false);
    }
}
