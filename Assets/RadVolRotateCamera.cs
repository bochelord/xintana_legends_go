using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RadVolRotateCamera : MonoBehaviour {


    public float angleCamera;
    public float movementCamera;


    //void Start()
    //{
    //    this.GetComponent<Camera>().orthographic = false;
    //    this.transform.DORotate(new Vector3(0f,angleCamera,0f),2);
    //    this.transform.DOMove(new Vector3(movementCamera, 1, -10), 2);
    //}






    public void Button_InvertCameraAngle()
    {
        this.GetComponent<Camera>().orthographic = false;
        angleCamera = -angleCamera;
        movementCamera = -movementCamera;
        moveCamera();
    }

    void moveCamera()
    {
        this.transform.DORotate(new Vector3(0f, angleCamera, 0f), 2);
        this.transform.DOMove(new Vector3(movementCamera, 1, -10), 2);
    }

    public void ResetCamera()
    {
        DOTween.KillAll();
        this.transform.position = new Vector3(0, 1, -10);
        this.GetComponent<Camera>().orthographic = true;
        this.transform.rotation = Quaternion.Euler(0,0,0);
    }
}
