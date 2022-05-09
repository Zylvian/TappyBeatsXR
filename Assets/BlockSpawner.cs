using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    public SequencerDriver leadBlock;
    public SequencerDriver bassBlock;
    public SequencerDriver drumsBlock;

    public Transform spawnHere;

    public SolverHandler masterSolver;

    //[RequireComponent(typeof(Rigidbody))]
    public SequentialSequencerBar masterMan;

    private int columns = 4;

    public void SpawnBass()
    {
        Transform target = masterSolver.TransformTarget;
        Vector3 newPos = target.position + target.forward * 0.6f;
        SequencerDriver childDriver = Instantiate(bassBlock, newPos, Quaternion.identity, spawnHere);

        //childDriver.transform.LookAt(target, target.up);
        //childDriver.transform.Rotate(Vector3.up, 180f);
        ContinueSpawn(childDriver);
        
    }

    public void SpawnLead()
    {
        SequencerDriver childDriver = Instantiate(leadBlock, new Vector3(0, 0, 0.5f), Quaternion.identity, spawnHere);
        ContinueSpawn(childDriver);
    }

    public void SpawnDrums()
    {
        Transform target = masterSolver.TransformTarget;
        Vector3 newPos = target.position + target.forward * 0.6f;
        SequencerDriver childDriver = Instantiate(drumsBlock, newPos, Quaternion.identity, spawnHere);
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


