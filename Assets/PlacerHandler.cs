using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerHandler : MonoBehaviour
{

    public Transform origParent;
    public Transform placedParent;

    private TapToPlace solverman;

    public SolverHandler masterSolver;

    public GameObject hider;

    private bool placed;

    private void Awake()
    {
        solverman = GetComponent<TapToPlace>();
    }

    public void StartPlaced()
    {

        // If the object is placed, snap back to face.
        if (placed)
        {
            solverman.StopPlacement();
            Debug.Log("stopping placement");
            transform.parent = origParent;
            BackToOrig();
            placed = false;
            hider.SetActive(false);
        }
        // If not, change the parent.
        else
        {
            transform.parent = placedParent.transform;
            
        }

    }

    public void AfterPlaced()
    {
        Debug.Log("AfterPlaced");
        placed = true;
        hider.SetActive(true);
    }

    public void BackToOrig()
    {
        Transform target = masterSolver.TransformTarget;
        transform.position = target.position + target.forward * 0.6f;
        transform.LookAt(target, target.up);
        transform.Rotate(Vector3.up, 180f);
        //transform.rotation = target.rotation.eulerAngles;
    }
}
