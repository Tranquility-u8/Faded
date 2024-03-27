using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSpike : SpikeBase
{

    [SerializeField] private Animator animator;

    [SerializeField] private bool isStop = false;

    [SerializeField] private float riseTimeout = 0.5f;
    [SerializeField] private float onTimeout = 1;
    [SerializeField] private float sinkTimeout = 0.5f;
    [SerializeField] private float offTimeout = 1;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetUpSpike();
    }

    private void SetUpSpike()
    {
        if (!isActivated)
            StartCoroutine(OnAnim());
        else
            StartCoroutine(OffAnim());
    }

    public void OnStop()
    {
        isStop = true;
    }

    IEnumerator OnAnim()
    {
        animator.Play("Rise");
        yield return new WaitForSeconds(riseTimeout);
        isActivated = true;
        yield return new WaitForSeconds(onTimeout);
        if(!isStop) 
            StartCoroutine(OffAnim());
    }

    IEnumerator OffAnim()
    {
        animator.Play("Sink");
        yield return new WaitForSeconds(sinkTimeout);
        isActivated = false;
        yield return new WaitForSeconds(offTimeout);
        if(!isStop)
            StartCoroutine(OnAnim());
    }
    void Update()
    {
        
    }
}
