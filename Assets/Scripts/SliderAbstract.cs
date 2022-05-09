using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderAbstract : MonoBehaviour
{
    [System.NonSerialized]
    public PinchSlider slider;

    private void Awake()
    {
        slider = GetComponent<PinchSlider>();
    }

    public float GetSliderValue()
    {
        // Base value is 0.5, goes between 0 and 100.
        float currVal = slider.SliderValue;

        return currVal;
    }
}
