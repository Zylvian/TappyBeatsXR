using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioMixer masterMixer;

    public void SetReverb(float sliderVal)
    {
        float converted = Mathf.Lerp(-10000, 2000, sliderVal);
        Debug.LogWarning(converted);
        masterMixer.SetFloat("ReverbLevel", converted);
    }

}
