using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioToSeq : MonoBehaviour
{

    Sequencer changeMan;

    /*[Min(1)]
    public int buttonAmount;*/

    public RowsConstructor prefabRow;

    public GameObject sequenceButtonPrefab;
    public GameObject playButtonPrefab;

    public TextMesh mrtkText;

    private int buttonAmount = 4;

    public void Build(AudioClip a)
    {
        //int buttonAmount = prefabRow.columns;
        changeMan = GetComponent<Sequencer>();
        //
        GridObjectCollection coll = GetComponent<GridObjectCollection>();



        for(int i = 0; i < buttonAmount; i++)
        {
            int localIndex = i;
            GameObject button = Instantiate(sequenceButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
            button.GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => RadioClick(localIndex));
        }

        AddPlayButton(a);

        coll.UpdateCollection();


        // Create sequence length equal to buttons.

        changeMan.sequence = new bool[buttonAmount];
        //changeMan.signatureLo = buttonAmount;
        changeMan.signatureLo = 4;
    }

    public void AddText(string soundName)
    {
        TextMesh something = Instantiate(mrtkText, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
        something.text = soundName.ToString();
    }

    public void AddPlayButton(AudioClip a)
    {
        GameObject something = Instantiate(playButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
        something.GetComponent<AudioSource>().clip = a;
    }

    public void SetColumnAmount(int columns)
    {
        buttonAmount = columns;
    }

    public void RadioClick(int nr)
    {
        changeMan.sequence[nr] = !changeMan.sequence[nr];
    }
}
