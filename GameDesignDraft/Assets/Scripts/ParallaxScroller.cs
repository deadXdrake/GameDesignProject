using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour

{
    public Renderer[] layers;
    public float[] speedMultiplier;
    private float previousXPositionNezuko;
    private float previousXPositionCamera;
    public Transform nezuko;
    public Transform mainCamera;
    private float[] offset;

    void Start()
    {
        offset = new float[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            offset[i] = 0.0f;
        }
        previousXPositionNezuko = nezuko.transform.position.x;
        previousXPositionCamera = mainCamera.transform.position.x;
    }

    void Update()
    {
        // if camera has moved
        if (Mathf.Abs(previousXPositionCamera - mainCamera.transform.position.x) > 0.001f)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                if (offset[i] > 1.0f || offset[i] < -1.0f)
                    offset[i] = 0.0f; //reset offset
                //float newOffset = nezuko.transform.position.x - previousXPositionNezuko; //fix relative to maincam instead of Nezuko. Now trippy because everything is fixed relativet to Nezuko
                float newOffset = mainCamera.transform.position.x - previousXPositionCamera;
                offset[i] = offset[i] + newOffset * speedMultiplier[i];
                layers[i].material.mainTextureOffset = new Vector2(offset[i], 0);
            }
        }
        //update previous pos
        previousXPositionNezuko = nezuko.transform.position.x;
        previousXPositionCamera = mainCamera.transform.position.x;
    }

}
