using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.8f;
    private float alphaSetMultiplier = 0.85f;

    private Transform player;

    private SpriteRenderer SR;
    private SpriteRenderer playerSR;

    private Color color;

    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        player.rotation = player.rotation;

        timeActivated = Time.time;

    }
        private void Update()
        {
            alpha *= alphaSetMultiplier;
            color = new Color(1f, 1f, 1f, alpha);
            SR.color = color;

            if(Time.time >= (timeActivated + activeTime)) 
            {
            //add
            }
        }
}
