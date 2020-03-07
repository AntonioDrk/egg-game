using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform cameraFollow;
    private float minSpeed = 1.5f;
    private float currSpeed = 2f;
    

    void Start()
    {
        if (transform.CompareTag("MainCamera"))
        {
            cameraFollow = transform;
        }
    }

    void Update()
    {
        KeepInBounds();
    }

    void KeepInBounds()
    {
        Vector3 currPos = cameraFollow.position;
        float distance = Vector2.Distance(currPos, target.position);

        currSpeed = 1.5f * distance < minSpeed ? minSpeed : 1.5f * distance;

        Vector3 moveStep = Vector3.MoveTowards(new Vector3(currPos.x, currPos.y, target.position.z),
            target.position, currSpeed * Time.deltaTime);
        
        cameraFollow.position = new Vector3(moveStep.x, moveStep.y, currPos.z);
    }
}
