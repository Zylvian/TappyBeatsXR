using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderAbstract : MonoBehaviour
{
    //[System.NonSerialized]
    public PinchSlider slider;

    //void Awake()
    //{
    //    slider = GetComponent<PinchSlider>();
    //}

    public float GetSliderValue()
    {
        float currVal = slider.SliderValue;
        return currVal;
    }
}
