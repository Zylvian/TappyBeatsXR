using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SliderToBPM : SliderAbstract
{

    //public SequencerDriver driverman;
    

    //[System.Serializable]
    //public class BpmEvent : UnityEvent<float> { }

    //[SerializeField]
    //public BpmEvent bpmUpdate;

    public TextMeshPro bpmText;
    //public HelperMetronome metro;

    //public SequencerDriver driverman;
    public SequentialSequencerBar seqMan;
    


    public void UpdateBpm()
    {

        // Base value is 50
        float currVal = GetSliderValue();

        if (currVal <= 0.1)
        {
            currVal = 0.1f;
        }

        int newBpm = (int)(currVal * 100 + 30);

        //driverman.SetBpm(currBpm);
        seqMan.SetBpm(newBpm);
        UpdateBpmText(newBpm);
        //metro.SetFlashBpm(currBpm);

        
    }

    private void UpdateBpmText(int bpm)
    {
        bpmText.text = bpm.ToString();
    }
}
