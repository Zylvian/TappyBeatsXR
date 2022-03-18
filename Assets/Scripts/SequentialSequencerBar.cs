using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SequentialSequencerBar : MonoBehaviour
{

    //public List<MusicFieldScript> fields = new List<MusicFieldScript>();
    public GameObject sequenceBar;
    private List<SequencerDriver> driversToPlay = new List<SequencerDriver>();
    //private SequencerDriver masterDriver;

    private int currField = 0;

    private bool isPlaying = false;

    public SequencerBase silenceFab;

    private int currBpm = 0;

    private static int defaultBPM = 80;

    public void Start()
    {

        //masterDriver = GetComponent<SequencerDriver>();

        foreach (Transform child in sequenceBar.transform)
        {
            SequencerDriver currDriver = child.gameObject.GetComponent<SequencerDriver>();
            driversToPlay.Add(currDriver);
        }
    }

    public void Play()
    {
        if (!isPlaying) {
            FetchDrivers();
            Debug.Log(driversToPlay.Count);
            driversToPlay[currField].Play();
            driversToPlay[currField].GetComponent<SeqBlockHandler>().ToggleMaterial(MatStates.Play);
            isPlaying = true;
        }
        else
        {
            Debug.Log("system already playing");
        }

    }

    public void Stop()
    {
        foreach(SequencerDriver driver in driversToPlay)
        {
            driver.Stop();
        }

        DumpDrivers();

        currField = 0;

        isPlaying = false;
    }

    public void FetchDrivers()
    {
        

        foreach (SequencerDriver driver in driversToPlay)
        {
            // If there is only the metronome/ticker on the driver, enable it.
            if(driver.sequencers.Length == 1)
            {
                driver.GetComponent<SeqBlockHandler>().TickToggle(true);
                
            }
            else
            {
                driver.GetComponent<SeqBlockHandler>().TickToggle(false);
            }

            driver.OnLoop += WhenSeqLoops;

        }

        SetBpmDefault();
    }

    public void DumpDrivers()
    {
        foreach (SequencerDriver driver in driversToPlay)
        {
            driver.OnLoop -= WhenSeqLoops;
        }
    }

    public void WhenSeqLoops()
    {
        //Debug.Log("TESTINGMAN HEI HEI HEI LOOP LOOP");
        Debug.LogWarning("Current driver that looped: " + currField);
        driversToPlay[currField].Stop();
        driversToPlay[currField].GetComponent<SeqBlockHandler>().ToggleMaterial(MatStates.Standard);
        currField++;
        Debug.LogWarning("Next driver to play: " + currField);
        if(currField > driversToPlay.Count - 1) { currField = 0; }
        Debug.LogWarning("Playing field: " + currField);
        driversToPlay[currField].Play();
        driversToPlay[currField].GetComponent<SeqBlockHandler>().ToggleMaterial(MatStates.Play);

    }

    public void SetBpm(int bpm)
    {
        //sequenceBar.transform.child
        //foreach(MusicFieldScript field in fields)
        foreach(Transform child in sequenceBar.transform)
        {
            foreach(SequencerDriver driver in child.GetComponent<SequencerDriver>().sequencers)
            {
                try
                {
                    int columns = driver.GetComponentInChildren<RowsConstructor>().columns;
                    driver.SetBpm(bpm * columns / 4);
                    Debug.Log(driver.GetComponentInChildren<RowsConstructor>().columns);
                }
                catch(NullReferenceException e)
                {
                    driver.SetBpm(bpm * 4 / 4);
                }
            }

        }

        currBpm = bpm;
    }

    public void SetBpmDefault()
    {
        if (currBpm == 0)
        {
            currBpm = defaultBPM;
        }

        SetBpm(currBpm);
    }

    //public void CalculateBpm(int bpm)
    //{
    //    // 4 is standard.
    //    int steps = 

    //    return bpm;
    //}

    public Sequencer CreateDummySequencer()
    {
        Sequencer dummyMan = new Sequencer();
        dummyMan.sequence = new bool[] { false, false, false, false };
        //dummyMan.OnLoop += WhenSeqLoops;
        return dummyMan;
    }

    public SequencerBase CreateDummySequencerSilence()
    {
        //Sequencer dummyMan = new Sequencer();
        //dummyMan.clip = Resources.Load<AudioClip>($"AUDIO/SILENCE.mp3");
        //dummyMan.sequence = new bool[] { true, true, true, true };
        ////dummyMan.OnLoop += WhenSeqLoops;
        //return dummyMan;
        return Instantiate<SequencerBase>(silenceFab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
    }

    //public SequencerDriver CreateDummyDriver()
    //{
    //    SequencerDriver driver = new SequencerDriver();
    //    Sequencer sequencer = CreateDummySequencer();
    //    driver.sequencers = new SequencerBase[]{ sequencer };
    //    return driver;
    //}
}
