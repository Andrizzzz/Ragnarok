using Lance.CoreSystem;
using UnityEngine;

public class BossDefeat : MonoBehaviour
{
    public GameObject boss;

    public GameObject portal;

    private Stats bossStats;

    void Start()
    {
        if (portal != null)
        {
            Debug.Log("Portal is found and set to inactive.");
            portal.SetActive(false);
        }
        else
        {
            Debug.LogError("Portal reference is not set!");
        }

        if (boss == null)
        {
            Debug.LogError("Boss reference is not set!");
        }
        else
        {
            bossStats = boss.GetComponentInChildren<Lance.CoreSystem.Stats>();
            if (bossStats != null)
            {
                bossStats.OnHealthZero += HandleBossDefeat;
                Debug.Log("Boss is found and stats are set.");
            }
            else
            {
                Debug.LogError("Boss does not have a Stats component!");
            }
        }
    }

    private void HandleBossDefeat()
    {
        Debug.Log("Boss is defeated.");
        if (portal != null)
        {
            Debug.Log("Activating portal.");
            portal.SetActive(true);
        }
        DestroyBoss();
    }

    private void DestroyBoss()
    {
        Destroy(boss);
        boss = null;
        Debug.Log("Boss has been destroyed.");
    }
}