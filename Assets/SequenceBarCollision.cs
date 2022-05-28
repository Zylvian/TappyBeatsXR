using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceBarCollision : MonoBehaviour
{
    // Start is called before the first frame update


    // Find closest Collisionthing
    private GameObject closest = null;
    private float closestDist = 0f;

    public Material hoverMat;
    public Material standardMat;

    private Transform origParent;

    float hoverVal = 0.4f;
    float standardVal = 1f;

    private void Awake()
    {
        origParent = transform.parent;
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag != "Sequence") { return; }

        float currDistance = Vector3.Distance(other.transform.position, transform.position);

        //Debug.Log("GO " + other.name + " is " + (currDistance*1000) + " units away");

        if (other.gameObject == closest)
        {
            closestDist = currDistance;
        }

        if (!closest || currDistance < closestDist)
        {
            if (closest) {
                SeqBlockHandler handler = closest.GetComponent<SeqBlockHandler>();
                handler.ToggleMaterial(MatStates.Standard);
                handler.RemoveBlock(this.transform.parent.gameObject);
            }

            closest = other.gameObject;
            closestDist = currDistance;

            //closest.GetComponent<MeshRenderer>().material = hoverMat;
            closest.GetComponent<SeqBlockHandler>().ToggleMaterial(MatStates.Hover);

            //Swap parent
            //transform.parent = other.gameObject.transform;

            //closest.GetComponent<Renderer>().sharedMaterial.SetFloat("_Smoothness", hoverVal);
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Sequence") { return; }

        //Debug.Log("exited " + other.gameObject.name);

        if(other.gameObject == closest)
        {
            SeqBlockHandler handler = closest.GetComponent<SeqBlockHandler>();
            handler.ToggleMaterial(MatStates.Standard);
            handler.RemoveBlock(this.transform.parent.gameObject);
            closest = null;

            //Swap parent
            transform.parent = origParent;
        }
    }

    public void LetGo()
    {
        if (closest)
        {
            // If there is space in the square, render new block and hide sequencer.
            if(closest.GetComponent<SeqBlockHandler>().RenderBlock(GetComponent<Renderer>()))
                closest.GetComponent<SeqBlockHandler>().AddBlock(this.transform.parent.gameObject);
            
        }
    }
}
