using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPlateAnimations : MonoBehaviour
{

    public Transform transman;

    private Vector3 minScale;
    private Vector3 maxScale;
    private float speed = 7f;
    private float scaleTime = 3f;

    private bool scaledDown;

    private float scaleFactor = 0.2f;


    private void Awake()
    {
        scaledDown = false;
    }

    void Start()
    {
        maxScale = transman.localScale;
        minScale = new Vector3(maxScale.x * scaleFactor, maxScale.y * scaleFactor, maxScale.z * scaleFactor);
    }

    // Start is called before the first frame update
    public void ScaleDown()
    {
        if (!scaledDown)
        {
            StartCoroutine(ScaleFunc(maxScale, minScale, scaleTime));
            scaledDown = true;
        }
            
        
    }

    public void ScaleUp()
    {
        if (scaledDown) { 
            StartCoroutine(ScaleFunc(minScale, maxScale, scaleTime));
            scaledDown = false;
        }
    }

    //IEnumerator Update()
    //{
    //    while (scaledUp)
    //    {
    //        yield return RepeatLerp(minScale, maxScale, duration);
    //        yield return RepeatLerp(maxScale, minScale, duration);
    //    }
    //}

    public IEnumerator ScaleFunc(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while(i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transman.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
