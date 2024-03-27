using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource door;
    [SerializeField] private AudioSource spike;
    [SerializeField] private AudioSource effect;
    [SerializeField] private AudioSource walk;
    [SerializeField] private AudioSource coin;


    [Header("Bgm")]
    [SerializeField] private AudioClip normalClip;
    [SerializeField] private AudioClip bossClip;

    [Header("Bullet")]
    [SerializeField] public AudioClip hitClip;

    [Header("Door")]
    [SerializeField] private AudioClip openDoorClip;
    [SerializeField] private AudioClip closeDoorClip;

    [Header("Spike")]
    [SerializeField] private AudioClip spikeClip;

    [Header("Monster")]
    [SerializeField] public AudioClip skeletonWalkClip;

    [Header("Effect")]
    [SerializeField] public AudioClip healClip;

    [Header("Walk")]
    [SerializeField] private AudioClip walkClip;

    [Header("Coin")]
    [SerializeField] private AudioClip coinClip;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Start()
    {
        inNormal();
    }

    public void inNormal()
    {
        bgm.clip = normalClip;
        bgm.Play();
    }

    public void inBattle()
    {
        bgm.clip = bossClip;
        bgm.Play();
    }

    public void inOpenDoor()
    {
        door.clip = openDoorClip;
        door.Play();
    }

    public void inCloseDoor()
    {
        door.clip = closeDoorClip;
        door.Play();
    }

    public void inSpike()
    {
        spike.clip = spikeClip;
        spike.Play();
    }

    public void inHeal()
    {
        effect.clip = healClip;
        effect.Play();
    }

    public void OnStartWalk()
    {
        walk.clip = walkClip;
        walk.Play();
    }

    public void OnStopWalk()
    {
        walk.Stop();
    }

    public void InCoins()
    {
        coin.clip = coinClip;
        coin.Play();
    }

    private void OnApplicationQuit()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
    }
}
