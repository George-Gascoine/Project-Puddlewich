using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
        var vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.LookAt = player.transform;
        vcam.Follow = player.transform;
    }
}
