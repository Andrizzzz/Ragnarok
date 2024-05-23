using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float respawnTime;

    private float respawnTimeStart;

    private bool respawn;

    private CinemachineVirtualCamera CVC;

    private void Start()
    {
        CVC = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
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
                // Spawn a new player object at the respawn point
                GameObject newPlayer = Instantiate(player, respawnPoint.position, respawnPoint.rotation);

                // Deactivate the old player object
                player.gameObject.SetActive(false);

                // Set the camera to follow the new player
                CVC.m_Follow = newPlayer.transform;

                // Reset respawn flag
                respawn = false;

                // Reset respawnTimeStart
                respawnTimeStart = Time.time;

                Debug.Log("Player respawned successfully.");
            }
            else
            {
                Debug.Log("Respawn time not yet reached.");
            }
        }
    }



    //orig
}