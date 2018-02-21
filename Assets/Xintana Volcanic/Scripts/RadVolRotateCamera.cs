using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RadVolRotateCamera : MonoBehaviour {


    public float angleCamera;
    public float movementCamera;

    private Camera camera;

    void Start()
    {
        camera = this.GetComponent<Camera>();
        //    this.GetComponent<Camera>().orthographic = false;
        //    this.transform.DORotate(new Vector3(0f,angleCamera,0f),2);
        //    this.transform.DOMove(new Vector3(movementCamera, 1, -10), 2);
    }






    public void Button_InvertCameraAngle()
    {
        camera.orthographic = false;
        angleCamera = -angleCamera;
        movementCamera = -movementCamera;
        moveCamera();
    }

    void moveCamera()
    {
        this.transform.DORotate(new Vector3(0f, angleCamera, 0f), 2).SetEase(Ease.InOutQuad);
        this.transform.DOMove(new Vector3(movementCamera, 1, -10), 2);
    }

    

    public void ResetCamera()
    {
        DOTween.KillAll();
        this.transform.position = new Vector3(0, 1, -10);
        camera.orthographic = true;
        this.transform.rotation = Quaternion.Euler(0,0,0);
    }

    public void MoveCameraToMainMenu()
    {
        this.transform.DOMove(new Vector3(-3.75f, 0.7f, -10f), 1);

        //DOTween.To(() => this.GetComponent<PerspectiveSwitcher>().orthographicSize, x => this.GetComponent<PerspectiveSwitcher>().orthographicSize = x, 3f, 1f);
        camera.DOOrthoSize(3f, 1f);
    }

    public void MoveCameraToGamePlay()
    {
        this.transform.DOMove(new Vector3(0f, 0.7f, -10f), 1);
        //DOTween.To(() => this.GetComponent<PerspectiveSwitcher>().orthographicSize, x => this.GetComponent<PerspectiveSwitcher>().orthographicSize = x, 5.63f, 1f);
        camera.DOOrthoSize(5.63f, 1f);
    }
}
