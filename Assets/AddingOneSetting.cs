using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddingOneSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        UnityEngine.XR.XRSettings.renderViewportScale = 0.7f;
    }

}
