using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{

    public SequentialSequencerBar[] loops;
    private int currWaiting;

    private int currBpm;

    private int defaultBpm = 80;

    private void Start()
    {
        currWaiting = loops.Length;
    }

    public void SetBpmDefaultAll()
    {
        foreach(SequentialSequencerBar loop in loops)
        {
            loop.SetBpmDefault();
        }
    }

    public void SetBpm(int bpm)
    {
        currBpm = bpm;
        foreach (SequentialSequencerBar loop in loops)
        {
            loop.SetBpm(bpm);
        }
    }

    public void Play()
    {
        foreach (SequentialSequencerBar loop in loops)
        {
            loop.PrepPlay();
            loop.Play();
        }
    }

    public void Stop()
    {
        foreach (SequentialSequencerBar loop in loops)
        {
            loop.Stop();
        }
    }

    internal void ReadyForNext()
    {
        currWaiting--;

        if(currWaiting == 0)
        {

            foreach (SequentialSequencerBar loop in loops)
            {
                loop.queueNow = true;
            }

            currWaiting = loops.Length;
        }


    }
}
