using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    public static event System.Action OnPlayerRespawn; // Static event for player respawn

    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private GameObject playerPrefab;  // Changed variable name to avoid confusion
    [SerializeField]
    private float respawnTime;

    private float respawnTimeStart;
    private bool respawn;
    private CinemachineVirtualCamera CVC;
    private GameObject currentPlayer;

    private void Start()
    {
        CVC = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        currentPlayer = GameObject.FindWithTag("Player"); // Find the initial player
        if (currentPlayer != null)
        {
            CVC.m_Follow = currentPlayer.transform;
        }
    }

    private void Update()
    {
        CheckRespawn();
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }


    private void CheckRespawn()
    {
        if (respawn)
        {
            if (Time.time >= respawnTimeStart + respawnTime)
            {
                if (currentPlayer != null)
                {
                    currentPlayer.SetActive(false); // Deactivate the old player
                }

                currentPlayer = Instantiate(playerPrefab, respawnPoint.position, respawnPoint.rotation); // Instantiate new player
                CVC.m_Follow = currentPlayer.transform; // Set camera to follow new player

                respawn = false;

                Debug.Log("Player respawned successfully.");

                // Trigger the respawn event
                OnPlayerRespawn?.Invoke();
            }
            else
            {
                Debug.Log("Respawn time not yet reached.");
            }
        }
    }
}
