using System.Collections;
using System.Collections.Generic;
using GifEncoder;
using System;
using System.IO;
using UnityEngine;

public class GIFRecordingManager : MonoBehaviour {

    public float recordTime = 10.0f;

    public Camera renderCamera;
    private RenderTexture renderTexture;
    private AnimatedGifEncoder gifEncoder;
    private float timepassed;

    // Use this for initialization
    void Start () {

        renderCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        // Configure the camera to render manually into the render texture:
        this.renderTexture = new RenderTexture(320, 240, 24);
        this.renderCamera.enabled = false;
        this.renderCamera.targetTexture = this.renderTexture;

        // Create a GIF encoder:
        this.gifEncoder = new AnimatedGifEncoder(Path.Combine(Application.persistentDataPath, "xintanalegendsgo_.gif"));
        this.gifEncoder.SetDelay(1000 / 30);

        timepassed = 0;
    }
	
	// Update is called once per frame
	void Update () {

        timepassed += Time.deltaTime;

        // Force the render camera, which we disabled earlier, to render a frame:
        this.renderCamera.Render();

        // Copy the render texture data into a temporary texture:
        RenderTexture.active = this.renderTexture;
        Texture2D frameTexture = new Texture2D(this.renderTexture.width, this.renderTexture.height, TextureFormat.RGB24, false);
        frameTexture.ReadPixels(new Rect(0, 0, this.renderTexture.width, this.renderTexture.height), 0, 0);

        // Add the current frame to the GIF:
        this.gifEncoder.AddFrame(frameTexture);

        // Destroy the temporary texture:
        UnityEngine.Object.Destroy(frameTexture);

        if (timepassed > recordTime)
        {
            this.gifEncoder.Finish();
            this.Quit();
        }

    }


    private void Quit()
    {
        Destroy(this.gameObject);
    }
}
