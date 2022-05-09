using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderToReverb : SliderAbstract
{

    public AudioManager amang;

    public void UpdateReverb()
    {
        // 0 to 1
        float currVal = GetSliderValue();

        amang.SetReverb(currVal);
    }
}
