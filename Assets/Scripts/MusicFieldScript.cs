using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class MusicFieldScript : MonoBehaviour
{

    public int id;
    //public List<SequencerBase> currDrivers = new List<SequencerDriver>();
    //public SequentialSequencer masterMan;

    public SequencerDriver fieldDriver;

    // Start is called before the first frame update
    void Start()
    {
        fieldDriver = GetComponent<SequencerDriver>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("SequencerDriver entered " + id);
        // Gets driver f
        SequencerBase driver = other.transform.parent.gameObject.GetComponent<SequencerDriver>();

        // Option 1.
        //currDrivers.Add(driver);

        // Option 2.
        List<SequencerBase> driverList = fieldDriver.sequencers.ToList();
        driverList.Add(driver);
        fieldDriver.sequencers = driverList.ToArray();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("SequencerDriver left " + id);
        SequencerDriver driver = other.transform.parent.gameObject.GetComponent<SequencerDriver>();

        // Option 1.
        //currDrivers.Add(driver);

        // Option 2.
        List<SequencerBase> driverList = fieldDriver.sequencers.ToList();
        driverList.Remove(driver);
        fieldDriver.sequencers = driverList.ToArray();
    }



    /*private void GetAudioFromDriver(SequencerDriver other)
    {
        foreach(Sequencer seq in other.sequencers)
        {
            bool[] asd = seq.sequence;
            AudioClip clip = seq.clip;

            AudioClip[] clips = new AudioClip[asd.Length];

            for(int i = 0; i < asd.Length; i++)
            {
                if (asd[i])
                {
                    clips[i] = clip;
                }
                else
                {
                    float[] sampleMan = new float[clip.samples * clip.channels];
                    AudioClip emptyClip = AudioClip.Create("Empty", sampleMan.Length/2, 2, 44100, false);
                    clips[i] = emptyClip;
                }
            }
            GetComponent<ClipToWaves>().GatherAudioData(clips);
            return;
        }
    }*/

}
