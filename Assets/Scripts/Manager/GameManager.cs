using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float respawnTime;

    private bool respawnInProgress;
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start()
    {
        cinemachineVirtualCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
    }

    public void Respawn()
    {
        if (!respawnInProgress)
        {
            respawnInProgress = true;
            Invoke("SpawnPlayer", respawnTime);
        }
    }

    private void SpawnPlayer()
    {
        if (playerPrefab != null && respawnPoint != null)
        {
            GameObject playerInstance = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
            cinemachineVirtualCamera.Follow = playerInstance.transform;
            respawnInProgress = false;
        }
        else
        {
            Debug.LogWarning("Player prefab or respawn point not assigned.");
        }
    }
}
