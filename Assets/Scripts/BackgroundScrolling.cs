using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour {

    public Renderer mainRenderer;
    public float speedMultiplier;          //The current speed multiplier
    private Vector2 offset;

    void Start()
    {
        //speedMultiplier = 1;
        //paused = true;
    }
    // Update is called once per frame
    void Update()
    {
        //if (!paused)
        //{
            offset = mainRenderer.material.mainTextureOffset;
            offset.x += speedMultiplier * Time.deltaTime;

            if (offset.x > 1)
                offset.x -= 1;

            mainRenderer.material.mainTextureOffset = offset;
        //}
    }
}
