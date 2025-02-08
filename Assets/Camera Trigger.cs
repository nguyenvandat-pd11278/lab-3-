using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera vcam1;
    public CinemachineVirtualCamera vcam2;
    public Transform door;
    private bool doorTriggered = false;

    void Start()
    {
        vcam1.Priority = 1;
        vcam2.Priority = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !doorTriggered)
        {
            doorTriggered = true;
            vcam1.Priority = 0;
            vcam2.Priority = 1;
            Debug.Log("Chuy?n sang camera 2");
        }
    }
}