using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HelperMetronome : MonoBehaviour
{
    public double bpm = 120.0F;
    public float gain = 0.5F;
    public int signatureHi = 4;
    public int signatureLo = 4;
    private double nextTick = 0.0F;
    private float amp = 0.0F;
    private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private int accent;
    private bool running = false;

    public Material onBeatMat;
    public Material offBeatMat;

    [SerializeField]
    public SequencerDriver mainDriver;

    private MeshRenderer changeMesh;

    private int tickCount = 0;
    private double jarleNextTick;

    private double jarleStartTick;

    

    IEnumerator Start()
    {
        enabled = false;
        yield return new WaitUntil(() => mainDriver.IsInitialized);
        enabled = true;

        changeMesh = GetComponent<MeshRenderer>();
        accent = signatureHi;
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        
        running = true;

        jarleStartTick = AudioSettings.dspTime;
        jarleNextTick = nextTick;
    }

    private void Update()
    {
        double samplesPerTick = sampleRate * 60.0F / bpm * 4.0F / signatureLo;
        double sample = AudioSettings.dspTime * sampleRate;

        if (sample > jarleNextTick)
        {
            tickCount++;
            jarleNextTick += samplesPerTick;
        }

        if(tickCount % 4 == 1)
        {
            Debug.Log("FLASH NOW!");
            changeMesh.material = onBeatMat;
        }
        else
        {
            changeMesh.material = offBeatMat;
        }
        
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        double samplesPerTick = sampleRate * 60.0F / bpm * 4.0F / signatureLo;
        double sample = AudioSettings.dspTime * sampleRate;
        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            float x = gain * amp * Mathf.Sin(phase);
            int i = 0;
            while (i < channels)
            {
                data[n * channels + i] += x;
                i++;
            }
            while (sample + n >= nextTick)
            {
                nextTick += samplesPerTick;
                amp = 1.0F;
                if (++accent > signatureHi)
                {
                    accent = 1;
                    amp *= 2.0F;
                }
                
                Debug.Log("Tick: " + accent + "/" + signatureHi);
            }
            phase += amp * 0.3F;
            amp *= 0.993F;
            n++;
        }
    }

    public void SetFlashBpm(int newBpm)
    {
        this.bpm = newBpm;
    }
}
