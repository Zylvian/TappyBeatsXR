using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SequentialSequencerFields: MonoBehaviour
{

    //public SequencerDriver[] segments;
    //public List<SequencerDriver> listman = new List<SequencerDriver>();
    public List<MusicFieldScript> fields = new List<MusicFieldScript>();
    private List<SequencerDriver> driversToPlay = new List<SequencerDriver>();
    // Start is called before the first frame update

    //public void newsegment(sequencerdriver driverman)
    //{
    //    listman.add(driverman);
    //}

    public SequencerDriver cloneDriver;

    private SequencerConstructor constructor = new SequencerConstructor();

    private int eventCounter;
    private int currField = 0;

    public void Play()
    {
        FetchDrivers();
        Debug.Log(driversToPlay.Count);
        driversToPlay[currField].Play();
    }

    public void Stop()
    {
        foreach(SequencerDriver driver in driversToPlay)
        {
            driver.Stop();
        }

        DumpDrivers();

        currField = 0;
    }

    public void FetchDrivers()
    {
        //foreach(MusicFieldScript field in fields)
        //{
        //    SequencerDriver currDriver = new SequencerDriver();
        //    currDriver.bpm = 90;
        //    //SequencerDriver currDriver = Instantiate(cloneDriver);

        //    List<SequencerDriver> fieldDrivers = field.currDrivers;

        //    currDriver.sequencers = fieldDrivers.ToArray();

        //    currDriver.OnLoop += TestingMan;
        //    driversToPlay.Add(currDriver);
        //}

        //Option 2.
        SequencerDriver masterDriver = GetComponent<SequencerDriver>();
        foreach (SequencerDriver driver in masterDriver.sequencers)
        {
            driver.OnLoop += WhenSeqLoops;
            driversToPlay.Add(driver);
        }

        //// Option 1.
        //foreach(MusicFieldScript field in fields)
        //{
        //    field.Play();
        //}
    }

    public void DumpDrivers()
    {
        //Option 2.
        SequencerDriver masterDriver = GetComponent<SequencerDriver>();
        foreach (SequencerDriver driver in masterDriver.sequencers)
        {
            driver.OnLoop -= WhenSeqLoops;
            driversToPlay.Remove(driver);
        }
    }

    public void WhenSeqLoops()
    {
        //Debug.Log("TESTINGMAN HEI HEI HEI LOOP LOOP");
        Debug.LogWarning("Current driver that looped: " + currField);
        driversToPlay[currField].Stop();
        currField++;
        Debug.LogWarning("Next driver to play: " + currField);
        if(currField > driversToPlay.Count - 1) { currField = 0; }
        Debug.LogWarning("Playing field: " + currField);
        driversToPlay[currField].Play();
        
    }

    public void SetBpm(int bpm)
    {

        foreach(MusicFieldScript field in fields)
        {
            foreach(SequencerDriver driver in field.GetComponent<SequencerDriver>().sequencers)
            {
                int columns = driver.GetComponentInChildren<RowsConstructor>().columns;
                driver.SetBpm(bpm*columns/4);
                Debug.Log(driver.GetComponentInChildren<RowsConstructor>().columns);
            }
            //SequencerDriver fieldDriver = field.GetComponent<SequencerDriver>();
            //foreach(SequencerDriver driver in fieldDriver.sequencers)
            //{
            //    driver.bpm = CalculateBpm(driver.bpm);
            //}
        }
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
        dummyMan.OnLoop += WhenSeqLoops;
        return dummyMan;
    }

    public SequencerDriver CreateDummyDriver()
    {
        SequencerDriver driver = new SequencerDriver();
        Sequencer sequencer = CreateDummySequencer();
        driver.sequencers = new SequencerBase[]{ sequencer };
        return driver;
    }
}
