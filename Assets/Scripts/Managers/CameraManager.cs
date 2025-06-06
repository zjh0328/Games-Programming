using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public CinemachineVirtualCamera cm;

    [Header("Camera Lens Info")]
    public float defaultCameraLensSize;
    public float targetCameraLensSize;
    public float cameraLensSizeChangeSpeed;

    [Header("Camera Screen Y Position Info")]
    public float defaultCameraYPosition;
    public float targetCameraYPosition;
    public float cameraYPositionChangeSpeed;
    private Player player;
    public CinemachineFramingTransposer ft { get; set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ft = cm.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Start()
    {
        player = PlayerManager.instance.player;
    }

}
