﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    AudioManager audioManager;
    private TrailRenderer trail;
    public ParticleSystem clickExplode;

    void Start()
    {
        audioManager = AudioManager.instance;               // instantiates AudioManager

        if (audioManager == null)
        {
            Debug.LogError("No AudioManager Found");
        }
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        trail = GetComponent<TrailRenderer>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            clickExplode.Play();                    // play particle effect
            audioManager.PlaySound("ObjectSnap");   // play sound
        }

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }
}
