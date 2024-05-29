using UnityEngine;

namespace Lance
{
    public class DoorDialogue : MonoBehaviour
    {
        public GameObject player;            // Reference to the player GameObject
        public GameObject door;              // Reference to the door GameObject
        public GameObject dialoguePanel;     // Dialogue panel to show when close to the door
        public float proximityDistance = 2f; // Distance within which the dialogue panel should be shown

        private Collider2D targetObjectCollider; // Collider of the door GameObject

        private void Start()
        {
            // Get the collider attached to the door GameObject
            targetObjectCollider = door.GetComponent<Collider2D>();

            // Hide the dialogue panel when the game starts
            HideDialoguePanel();
            Debug.Log("Dialogue panel hidden at the start of the game.");
        }


        private void Update()
        {
            if (IsPlayerNearDoor())
            {
                ShowDialoguePanel();
            }
            else
            {
                HideDialoguePanel();
            }
        }

        private bool IsPlayerNearDoor()
        {
            // Check if the distance between player and door is less than or equal to proximityDistance
            // Also check if the target object's collider is enabled
            return Vector3.Distance(player.transform.position, door.transform.position) <= proximityDistance
                && targetObjectCollider.enabled;
        }

        private void ShowDialoguePanel()
        {
            dialoguePanel.SetActive(true);
        }

        private void HideDialoguePanel()
        {
            dialoguePanel.SetActive(false);
        }
    }
}
