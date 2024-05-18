using UnityEngine;
using UnityEngine.EventSystems;

namespace Lance
{
    public class EnsureSingleEventSystem : MonoBehaviour
    {
        void Awake()
        {
            // Find all EventSystem instances in the scene
            EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();

            // If more than one EventSystem is found, destroy the duplicates
            if (eventSystems.Length > 1)
            {
                for (int i = 1; i < eventSystems.Length; i++)
                {
                    Destroy(eventSystems[i].gameObject);
                }
            }
        }
    }
}

