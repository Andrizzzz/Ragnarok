using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;

    private float oldOrthographicSize;

    void Start()
    {
        oldOrthographicSize = Camera.main.orthographicSize;
    }

    void LateUpdate()
    {
        float newOrthographicSize = Camera.main.orthographicSize;

        if (newOrthographicSize != oldOrthographicSize)
        {
            if (onCameraTranslate != null)
            {
                // Calculate delta movement based on the difference in orthographic size
                float delta = oldOrthographicSize - newOrthographicSize;
                onCameraTranslate(delta);
            }

            oldOrthographicSize = newOrthographicSize;
        }
    }
}
