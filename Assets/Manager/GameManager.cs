using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float respawnDelay = 1f;

    private bool respawning = false;
    private GameObject currentPlayer;

    private CinemachineVirtualCamera CVC;

    private void Start()
    {
        CVC = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        SpawnPlayer();
    }

    private void Update()
    {
        if (respawning)
        {
            if (currentPlayer == null) // Check if the current player object has been destroyed
            {
                respawning = false;
                Invoke("SpawnPlayer", respawnDelay);
            }
        }
    }

    public void Respawn()
    {
        if (!respawning)
        {
            respawning = true;
            Destroy(currentPlayer); // Destroy the current player object
        }
    }

    private void SpawnPlayer()
    {
        currentPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        if (CVC != null)
        {
            CVC.Follow = currentPlayer.transform;
        }
        else
        {
           UnityEngine.Debug.LogError("Cinemachine Virtual Camera is not assigned.");
        }
    }
}
