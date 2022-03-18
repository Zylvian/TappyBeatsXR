using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSequence : MonoBehaviour
{
    //List<bool> lowNotes = new List<bool>();
    //List<bool> highNotes = new List<bool>();

    public AudioClip clip;
    public bool[] notes = new bool[4];
    public GameObject myPrefab;

    // Start is called before the first frame update
    public void Start()
    {

        Vector3 spawnpos = this.gameObject.transform.position;
        spawnpos += new Vector3(-0.07f, -0.06f, -0.049f);

        GetComponent<Sequencer>().sequence = notes;
        GetComponent<Sequencer>().SetAudioClip(clip);

        foreach (bool eta in notes)
        {
            if (eta)
            {
                GameObject newCube = Instantiate(myPrefab, spawnpos, Quaternion.identity);
                newCube.transform.parent = gameObject.transform;
                //newCube.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            }

            spawnpos += new Vector3(0.035f, 0, 0);
        }

    }
}
