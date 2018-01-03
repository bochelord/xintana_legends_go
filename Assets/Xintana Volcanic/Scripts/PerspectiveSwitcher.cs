﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MatrixBlender))]
public class PerspectiveSwitcher : MonoBehaviour
{
    private Matrix4x4 ortho,
                        perspective;
    public float fov = 60f,
                        near = .3f,
                        far = 1000f,
                        orthographicSize = 50f,
                        blendDuration = 1f;
    private float aspect;
    private MatrixBlender blender;
    private bool orthoOn;

    void Start()
    {
        aspect = (float)Screen.width / (float)Screen.height;
        ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
        perspective = Matrix4x4.Perspective(fov, aspect, near, far);
        Camera.main.projectionMatrix = ortho;
        orthoOn = true;
        blender = (MatrixBlender)GetComponent(typeof(MatrixBlender));
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        orthoOn = !orthoOn;
    //        if (orthoOn)
    //            blender.BlendToMatrix(ortho, 1f);
    //        else
    //            blender.BlendToMatrix(perspective, 1f);
    //    }
    //}


    public void TogglePerspective()
    {
        orthoOn = !orthoOn;
        if (orthoOn)
            blender.BlendToMatrix(ortho, blendDuration);
        else
            blender.BlendToMatrix(perspective, blendDuration);
    }
}