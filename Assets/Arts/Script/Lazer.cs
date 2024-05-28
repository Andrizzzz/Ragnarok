using Lance.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public GameObject lazer;
    public Transform whereToSpawn;

    private void Start()
    {
        StartCoroutine(ToggleLazer());
    }

    private IEnumerator ToggleLazer()
    {
        while (true)
        {
            lazer.SetActive(false);
            yield return new WaitForSeconds(2f);
            Instantiate(lazer, whereToSpawn.position, whereToSpawn.rotation);
            lazer.SetActive(true);
            yield return new WaitForSeconds(2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(10); // or any other amount you want to deal
            }
        }
    }
}
