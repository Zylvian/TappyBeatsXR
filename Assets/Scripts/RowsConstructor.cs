using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;




public class RowsConstructor : MonoBehaviour
{

    [Min(1)]
    public int rows;

    // This is set in RadioToSeq
    [NonSerialized]
    public int columns = 4;

    public Sequencer rowPrefab;
    public SequencerDriver masterDriver;

    

    public Sound sound;


    [NonSerialized]
    public List<Sequencer> seqs;

    // Start is called before the first frame update
    public void Build()
    {

        // Load all audio files and create a row for each one.
        AudioClip[] textures = Resources.LoadAll<AudioClip>($"AUDIO/{sound}");
        Array.Reverse(textures);

        //Sequencer[] seqs = new Sequencer[textures.Length];

        seqs = new List<Sequencer>();

        foreach(AudioClip a in textures)
        {
            // Create new row for sound.


            Sequencer newRow = Instantiate(rowPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);

            RadioToSeq rad_seq = newRow.GetComponent<RadioToSeq>();
            rad_seq.SetColumnAmount(columns);

            rad_seq.AddText(a.name);
            
            rad_seq.Build(a);

            seqs.Add(newRow);
            
            // Set sequencer sound.
            newRow.SetAudioClip(a);

        }


        GridObjectCollection coll = GetComponent<GridObjectCollection>();
        coll.Rows = rows;

        coll.UpdateCollection();


        // Fix sequencer driver
        
        masterDriver.sequencers = seqs.ToArray();
    }
}
