using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Changes camera size when resolution is changed;
/// </summary>
public class CameraResolution : MonoBehaviour
{
    float resolution;
    public float defaultCameraSize;
    public float defaultResolution = 9.0F/16.0F;

    void Start()
    {
        resolution = (float)Screen.width / Screen.height;
    }

    void Update()
    {
        float newResolution = (float)Screen.width / Screen.height;
        if (resolution != newResolution)
        {
            resolution = newResolution;
            float cameraScale;
            if (resolution > defaultResolution) cameraScale = defaultResolution / resolution;
            else if (resolution < defaultResolution) cameraScale = resolution / defaultResolution;
            else cameraScale = defaultCameraSize;
            Camera.main.orthographicSize = defaultCameraSize * cameraScale;
            GlobalVariables.playAreaSize = Camera.main.orthographicSize * Camera.main.aspect - 0.4F;
        }
    }
    public static bool IsColliderFullyInCameraView(Collider2D collider)
    {
        if (collider == null) return false;
        float colliderFarEndPosition = Mathf.Abs(collider.transform.position.x) + collider.bounds.extents.x;
        return (Camera.main.orthographicSize * Camera.main.aspect > colliderFarEndPosition);
    }
}
