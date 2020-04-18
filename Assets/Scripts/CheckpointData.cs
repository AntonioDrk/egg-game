﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckpointData
{
    public float[] position;
    public int points;

    public CheckpointData(Vector3 checkPointPosition)
    {
        position = new float[3];
        position[0] = checkPointPosition.x;
        position[1] = checkPointPosition.y;
        position[2] = checkPointPosition.z;

        points = GameManager.Instance.Points;
    }
}