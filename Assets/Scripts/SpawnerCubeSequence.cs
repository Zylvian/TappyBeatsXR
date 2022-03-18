using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCubeSequence : MonoBehaviour
{
    public GameObject rowPrefab;
    static int sequenceRows = 4;
    public AudioClip[] clips = new AudioClip[sequenceRows];
    

    // Start is called before the first frame update
    void OnAwake()
    {

        Vector3 heightPlus = new Vector3(0, 0.035f, 0);
        for (int i = 0; i < sequenceRows; i++)
        {
            GameObject newOne = Instantiate(rowPrefab, this.gameObject.transform.position + heightPlus, Quaternion.identity);
            newOne.transform.parent = gameObject.transform;
            GetComponent<SequencerDriver>().sequencers[i] = newOne.GetComponent<Sequencer>();
            newOne.GetComponent<Sequencer>().SetAudioClip(clips[i]);
        }
    }
}
