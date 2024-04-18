using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private GameObject playerPrefab; // Change the type to GameObject
    [SerializeField]
    private float respawnTime; // Add this variable

    private float respawnTimeStart;

    private bool respawn;

    private CinemachineVirtualCamera CVC;

    private void Start()
    {
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    private void Update()
    {
        CheckRespawn();
    }

    private void CheckRespawn()
    {
        if (Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            // Instantiate the new player object at the respawn point
            GameObject newPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
            // Transfer the velocity from the old player to the new one
            Rigidbody2D oldPlayerRB = FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>();
            Rigidbody2D newPlayerRB = newPlayer.GetComponent<Rigidbody2D>();
            newPlayerRB.velocity = oldPlayerRB.velocity;

            // Make the new player the target of the virtual camera
            CVC.m_Follow = newPlayer.transform;

            respawn = false;
        }
    }
}
