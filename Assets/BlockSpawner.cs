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
    //public SequentialSequencerBar masterMan;

    public LoopManager loopManager;

    public SliderToBPM bpmRef;

    private int columns = 4;

    public void SpawnBass()
    {

        //SequencerDriver childDriver = Instantiate(bassBlock, newPos, Quaternion.identity, spawnHere);

        SequencerDriver childDriver = SpawnInstrument(bassBlock);
        ContinueSpawn(childDriver);
        
    }

    public void SpawnLead()
    {
        SequencerDriver childDriver = SpawnInstrument(leadBlock);
        ContinueSpawn(childDriver);
    }

    public void SpawnDrums()
    {
        SequencerDriver childDriver = SpawnInstrument(drumsBlock);  
        ContinueSpawn(childDriver);
    }

    public SequencerDriver SpawnInstrument(SequencerDriver instrument)
    {
        Transform target = masterSolver.TransformTarget;
        Vector3 newPos = target.position + target.forward * 0.6f + new Vector3(0,0,-0.03f); //new Vector3(-0.3f, 0.05f, 0.05f)

        SequencerDriver childDriver = Instantiate(instrument, newPos, Quaternion.identity, spawnHere);
        childDriver.SetBpm(bpmRef.GetBpm());
        return childDriver;
        //return Instantiate(bassBlock, spawnHere, spawnHere.forward.);
        //return Instantiate(instrument, spawnHere);
    }

    public void ContinueSpawn(SequencerDriver childDriver)
    {
        RowsConstructor asd = childDriver.GetComponentInChildren<RowsConstructor>();

        //

        asd.columns = columns;
        asd.Build(childDriver.gameObject.transform);

        //SequencerDriver masterDriver = GetComponent<SequencerDriver>();
        //List<SequencerBase> currList = masterDriver.sequencers.ToList();

        //currList.Add(childDriver);

        // Updates bpm for all blocks
        loopManager.SetBpmDefaultAll();
        //GetComponent<SequentialSequencerBar>().SetBpmDefault();
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


