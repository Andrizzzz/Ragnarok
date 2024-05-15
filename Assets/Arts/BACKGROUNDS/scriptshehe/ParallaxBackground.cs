using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;
    private List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    private Vector3 previousCameraPosition;

    private void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();

        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;

        SetLayers();

        // Initialize previousCameraPosition
        if (parallaxCamera != null)
            previousCameraPosition = parallaxCamera.transform.position;
    }

    private void SetLayers()
    {
        parallaxLayers.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null)
            {
                layer.name = "Layer-" + i;
                parallaxLayers.Add(layer);
            }
        }
    }

    private void LateUpdate()
    {
        // Calculate delta movement of the camera
        Vector3 delta = parallaxCamera.transform.position - previousCameraPosition;
        previousCameraPosition = parallaxCamera.transform.position;

        // Move the layers based on the camera's movement
        Move(delta.x);

        
    }


    private void Move(float delta)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(delta);
        }
    }
}
