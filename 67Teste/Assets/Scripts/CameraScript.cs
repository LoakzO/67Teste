using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Movement")]
    public Transform spot;

    void Start()
    {
        UpdateFrameRate();
    }

    void Update()
    {
        Follow();
    }

    void Follow()
    {
        transform.position = Vector3.Lerp(transform.position, spot.position, 4 * Time.deltaTime);
    }

    void UpdateFrameRate()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }
}
