using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInsidePlay : MonoBehaviour
{

    public AudioSource master;
    private int triggerCount = 0;

    public List<AudioSource> slaves;

    private IEnumerator coroutine;

    double initLatency = .1d;

    // Start is called before the first frame update
    void Start()
    {
        
        foreach (Transform child in transform)
        {
            slaves.Add(child.gameObject.GetComponent<AudioSource>());
        }

        coroutine = SyncSources();
        StartCoroutine(coroutine);

        /*Debug.Log("Queuing two plays");

        double playTime = AudioSettings.dspTime + initLatency;
        master.PlayDelayed((float)playTime);
        playTime += (double)master.clip.length;
        master.PlayDelayed((float)playTime);*/
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerCount++;
        Debug.Log("BRRR");
        if (!master.isPlaying)
        {
            master.Play();
        }
        other.gameObject.GetComponent<AudioSource>().Play();
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<AudioSource>().Stop();
        triggerCount--;
        if(triggerCount == 0)
        {
            master.Stop();
            Debug.Log("master stopped playing");
        }
    }

    private IEnumerator SyncSources()
    {
        while (true)
        {
            foreach (var slave in slaves)
            {
                if (master.isPlaying) { slave.timeSamples = master.timeSamples; }
                
                yield return null;
            }
        }
    }
}
