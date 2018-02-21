using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TextTimerIntro : MonoBehaviour {

    private Text timerText;
    private float timepassed = 0;


    private void Start()
    {
        timerText = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        timepassed += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(timepassed);

        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
    }
}
