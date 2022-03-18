using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeqBlockHandler : MonoBehaviour
{

    private SequencerDriver fieldDriver;
    public SequencerDriver tickDriver;
    public GameObject seqBlockPiece;

    private MaterialPropertyBlock _propBlock;
    private Renderer _renderer;

    private Renderer[] stacks = new Renderer[4];

    // Start is called before the first frame update
    void Awake()
    {
        fieldDriver = GetComponent<SequencerDriver>();
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();

        ToggleMaterial(MatStates.Standard);
    }

    public void AddBlock(GameObject seqBlock)
    {
        SequencerBase driver = seqBlock.GetComponent<SequencerDriver>();

        List<SequencerBase> driverList = fieldDriver.sequencers.ToList();
        if (!driverList.Contains(driver))
        {
            driverList.Add(driver);
            fieldDriver.sequencers = driverList.ToArray();
        }


        //seqBlock.SetActive(false);
    }

    public void RemoveBlock(GameObject seqBlock)
    {
        SequencerBase driver = seqBlock.GetComponent<SequencerDriver>();

        List<SequencerBase> driverList = fieldDriver.sequencers.ToList();
        driverList.Remove(driver);
        fieldDriver.sequencers = driverList.ToArray();
    }

    public void TickToggle(bool enable)
    {
        tickDriver.Mute(!enable);
    }

    public void ToggleMaterial(float smoothVal)
    {
        _propBlock.SetFloat("_Smoothness", smoothVal);
        _renderer.SetPropertyBlock(_propBlock);
    }

    internal bool RenderBlock(Renderer renderer)
    {

        Bounds renderBounds = GetComponent<Renderer>().bounds;
        Debug.Log(renderBounds);
        Debug.Log(renderBounds.size);
        Vector3 objectScale = new Vector3(renderBounds.size.x/4, renderBounds.size.y, renderBounds.size.z);
        objectScale.Scale(new Vector3(0.93f, 0.93f, 0.93f));

        GameObject blockman = Instantiate(seqBlockPiece, transform.position, transform.rotation);
        blockman.GetComponent<ObjectManipulator>().OnManipulationStarted
            .AddListener(delegate { RestoreSeq(renderer, blockman);});

        blockman.transform.localScale = objectScale;
        blockman.GetComponent<Renderer>().material = renderer.material;
        blockman.transform.parent = this.transform;

        // Move correctly

        Bounds blockBounds = blockman.GetComponent<MeshFilter>().mesh.bounds;


        Bounds localBounds = GetComponent<MeshFilter>().mesh.bounds;

        //(-(localBounds.size.x / 2.0f) + (blockBounds.size.x / 2.0f))

        for(int i = 0; i<stacks.Length; i++)
        {
            if(stacks[i] == null) {

                // Leftmost corner + half size of cube + center offset
                //+(localBounds.size.x / (4 - i))
                //Vector3 newPos = new Vector3(((localBounds.min.x) - (localBounds.size.x / 8f) ),
                float xCalc = ((localBounds.min.x) + (localBounds.size.x / 4) - (localBounds.size.x / 8f));
                Vector3 newPos = new Vector3( xCalc + (i*(localBounds.size.x / 4)),
                0f,
                0f);

                blockman.transform.localPosition = newPos;

                foreach (Renderer r in renderer.transform.parent.GetComponentsInChildren<Renderer>())
                    r.enabled = false;

                //renderer.enabled = false;

                stacks[i] = renderer;
                return true;
            }

        }
        return false;

        //Debug.Log(blockBounds.size.x);
        //Debug.Log(localBounds.size.x);

        //Debug.Log(blockBounds.size);
        //Debug.Log(blockman.GetComponent<Renderer>().bounds.size);
    }

    public void RestoreSeq(Renderer toRestore, GameObject toRemove)
    {
        //Renderer currRenderer = Array.Find(stacks, ele => ele == toRestore);
        int indexman = Array.FindIndex(stacks, ele => ele == toRestore);
        Renderer currRenderer = stacks[indexman];
        foreach (Renderer r in currRenderer.transform.parent.GetComponentsInChildren<Renderer>())
            r.enabled = true;

        stacks[indexman] = null;
        Destroy(toRemove);
    }
}
