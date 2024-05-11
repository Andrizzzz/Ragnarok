using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    private List<Collider2D> currentOneWayPlatforms = new List<Collider2D>();

    [SerializeField] private Collider2D playerCollider;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentOneWayPlatforms.Count > 0)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            Collider2D platformCollider = collision.collider;
            if (!currentOneWayPlatforms.Contains(platformCollider))
            {
                currentOneWayPlatforms.Add(platformCollider);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            Collider2D platformCollider = collision.collider;
            if (currentOneWayPlatforms.Contains(platformCollider))
            {
                currentOneWayPlatforms.Remove(platformCollider);
            }
        }
    }

    private IEnumerator DisableCollision()
    {
        foreach (var platformCollider in currentOneWayPlatforms)
        {
            Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        }
        yield return new WaitForSeconds(0.25f);
        foreach (var platformCollider in currentOneWayPlatforms)
        {
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        }
    }
}
  


