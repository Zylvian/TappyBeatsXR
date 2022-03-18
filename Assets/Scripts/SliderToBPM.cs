using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SliderToBPM : MonoBehaviour
{

    //public SequencerDriver driverman;
    

    [System.Serializable]
    public class BpmEvent : UnityEvent<float> { }

    [SerializeField]
    public BpmEvent bpmUpdate;

    public TextMeshPro bpmText;
    public HelperMetronome metro;

    //public SequencerDriver driverman;
    public SequentialSequencerBar seqMan;

    private PinchSlider slider;

    private void Awake()
    {
        slider = GetComponent<PinchSlider>();
    }
    public void UpdateBpm()
    {

        // Base value is 50
        int currBpm = GetSliderBpm();

        //driverman.SetBpm(currBpm);
        seqMan.SetBpm(currBpm);
        UpdateBpmText(currBpm);
        //metro.SetFlashBpm(currBpm);

        
    }

    private int GetSliderBpm()
    {
        // Base value is 0.5, goes between 0 and 100.
        float currVal = slider.SliderValue;

        if (currVal <= 0.1)
        {
            currVal = 0.1f;
        }

        int newBpm = (int) (currVal * 100 + 30);
        return newBpm;
    }

    private void UpdateBpmText(int bpm)
    {
        bpmText.text = bpm.ToString();
    }
}
