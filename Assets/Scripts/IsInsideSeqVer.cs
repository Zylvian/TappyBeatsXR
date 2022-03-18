using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class IsInsideSeqVer : MonoBehaviour
{

    SequencerDriver driverman;

    // Start is called before the first frame update
    void Start()
    {
        driverman = this.gameObject.transform.parent.GetComponent<SequencerDriver>();
        driverman.Play();
        foreach(SequencerDriver s in driverman.sequencers)
        {
            s.Mute(true);
        }
        
        

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BRRR");
        SequencerDriver driver = other.transform.parent.gameObject.GetComponent<SequencerDriver>();
        driver.Mute(false);
        //GetAudioFromDriver(driver);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("BRRR");
        other.transform.parent.gameObject.GetComponent<SequencerDriver>().Mute(true);
    }

    private void GetAudioFromDriver(SequencerDriver other)
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
    }

}
