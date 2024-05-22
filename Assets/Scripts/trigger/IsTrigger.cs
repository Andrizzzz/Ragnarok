using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

namespace Lance
{
    public class IsTrigger : MonoBehaviour
    {
        public CameraShaker cameraShaker; // Reference to the CameraShaker component
        public ShakeData earthquakeShakeData; // Shake data for the earthquake effect

        void Start()
        {
            if (cameraShaker == null)
            {
                Debug.LogError("CameraShaker reference is missing.");
            }
            if (earthquakeShakeData == null)
            {
                Debug.LogError("ShakeData reference is missing.");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) // Replace "Player" with the tag of the object that triggers the earthquake
            {
                Debug.Log("Trigger detected with Player.");
                cameraShaker.Shake(earthquakeShakeData);
            }
            else
            {
                Debug.Log("Trigger detected with non-Player: " + other.tag);
            }
        }
    }
}
