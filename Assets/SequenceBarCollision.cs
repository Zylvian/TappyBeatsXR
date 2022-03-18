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

    float hoverVal = 0.4f;
    float standardVal = 1f;

    //private void OnCollisionStay(Collision collision)
    //{
    //    Debug.Log("we staying");
    //    foreach(ContactPoint contact in collision.contacts)
    //    {
    //        Debug.DrawRay(contact.point, contact.normal, Color.white);

    //        float currDistance = Vector3.Distance(contact.otherCollider.transform.position, transform.position);
    //        Debug.Log("GO " + contact.otherCollider.name + " is " + currDistance + " units away");

    //        if (!closest || currDistance < closestDist)
    //        {

    //            closest = contact.otherCollider.gameObject;
    //            closestDist = currDistance;
    //            //contact.otherCollider.gameObject.GetComponent<MeshRenderer>().material = hoverMat;
    //        }
    //    }
    //}


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
            if (closest) { closest.GetComponent<SeqBlockHandler>().ToggleMaterial(MatStates.Standard); }

            closest = other.gameObject;
            closestDist = currDistance;

            //closest.GetComponent<MeshRenderer>().material = hoverMat;
            closest.GetComponent<SeqBlockHandler>().ToggleMaterial(MatStates.Hover);
            //closest.GetComponent<Renderer>().sharedMaterial.SetFloat("_Smoothness", hoverVal);
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Sequence") { return; }

        if(other.gameObject == closest)
        {
            //closest.GetComponent<MeshRenderer>().material = standardMat;
            //closest.GetComponent<Renderer>().sharedMaterial.SetFloat("_Smoothness", standardVal);
            closest.GetComponent<SeqBlockHandler>().ToggleMaterial(MatStates.Standard);
            closest = null;
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
