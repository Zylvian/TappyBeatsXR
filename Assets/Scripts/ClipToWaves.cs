using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipToWaves : MonoBehaviour
{

    public GameObject audioBar;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource src = GetComponent<AudioSource>();
        GatherAudioData(src.clip);
    }

    void GatherAudioData(AudioClip clip) { 
        float[] samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);

        Debug.Log("Sample length = " + samples.Length);

        int squares = 32;
        int sampleSpace = samples.Length / squares;

        float[] visSamples = new float[squares];

        for (int i = 0; i < visSamples.Length; ++i)
        {
            //Debug.Log("Getting data from point nr: " + (i * sampleSpace));
            visSamples[i] = samples[i*sampleSpace];
        }

        SpawnAudioSquares(visSamples);
    }

    public void GatherAudioData(AudioClip[] clips)
    {

        int length = 0;
        foreach(AudioClip clip in clips)
        {
            length += clip.samples * clip.channels;
        }

        float[] samples = new float[length];
        length = 0;
        foreach (AudioClip clip in clips)
        {
            float[] buffer = new float[clip.samples * clip.channels];
            clip.GetData(buffer, 0);
            buffer.CopyTo(samples, length);
            length += buffer.Length;
        }

        Debug.Log("Sample length = " + samples.Length);

        int squares = 64;
        int sampleSpace = samples.Length / squares;

        float[] visSamples = new float[squares];

        for (int i = 0; i < visSamples.Length; ++i)
        {
            //Debug.Log("Getting data from point nr: " + (i * sampleSpace));
            visSamples[i] = samples[i * sampleSpace];
        }

        SpawnAudioSquares(visSamples);
    }

    /*void CreateAudioSquares(float[] visSamples)
    {
        Vector3 curPos = transform.position;

        foreach(float now in visSamples)
        {
            float yScale = Mathf.Abs(now);
            GameObject newMan = Instantiate(audioBar, curPos, Quaternion.identity);
            newMan.transform.localScale += (new Vector3(0, yScale, 0));
            newMan.transform.position += new Vector3(0, 1, 0) * (yScale / 2);
            curPos += new Vector3(0.03f, 0, 0);
        }
    }*/

    void SpawnAudioSquares(float[] visSamples)
    {

        int num = visSamples.Length;

        Vector3 center = transform.position;

        for (int i = 0; i < visSamples.Length; i++) { 
            float now = visSamples[i];
            float currAng = (i) * (360 / visSamples.Length);
        //foreach (float now in visSamples){
            float yScale = Mathf.Abs(now);

            Vector3 pos = RandomCircle(center, 0.2f, currAng);
            //Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            //Quaternion rot = Quaternion.identity;
            Quaternion rot = Quaternion.Euler(0,0 , -currAng);
            GameObject newMan = Instantiate(audioBar, pos, rot);
            
            newMan.transform.localScale += (new Vector3(0, yScale, 0));
        }

    }

    Vector3 RandomCircle(Vector3 center, float radius, float ang)
    {
        //float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

}
