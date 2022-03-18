using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    public SequencerDriver leadBlock;
    public SequencerDriver bassBlock;
    public SequencerDriver drumsBlock;

    //[RequireComponent(typeof(Rigidbody))]
    public SequentialSequencerBar masterMan;

    private int columns = 4;

    public void SpawnBass()
    {
        SequencerDriver childDriver = Instantiate(bassBlock, new Vector3(0, 0, 0.5f), Quaternion.identity, this.transform);
        ContinueSpawn(childDriver);
        
    }

    public void SpawnLead()
    {
        SequencerDriver childDriver = Instantiate(leadBlock, new Vector3(0, 0, 0.5f), Quaternion.identity, this.transform);
        ContinueSpawn(childDriver);
    }

    public void SpawnDrums()
    {
        SequencerDriver childDriver = Instantiate(drumsBlock, new Vector3(0, 0, 0.5f), Quaternion.identity, this.transform);
        ContinueSpawn(childDriver);
    }

    public void ContinueSpawn(SequencerDriver childDriver)
    {
        RowsConstructor asd = childDriver.GetComponentInChildren<RowsConstructor>();

        //

        asd.columns = columns;
        asd.Build();

        SequencerDriver masterDriver = GetComponent<SequencerDriver>();
        List<SequencerBase> currList = masterDriver.sequencers.ToList();

        currList.Add(childDriver);

        // Updates bpm for all blocks
        GetComponent<SequentialSequencerBar>().SetBpmDefault();
        //masterDriver.sequencers = currList.ToArray();
    }

    public void IncreaseColumns()
    {
        columns++;
        Debug.Log(columns + " columns");
    }
    
    public void DecrementColumns()
    {
        columns--;
    }
}
