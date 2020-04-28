using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckpointData
{
    public int id;
    public float[] position;
    public float[] eggPosition;
    public bool[] stars;
    public int points;

    public CheckpointData(Vector3 checkPointPosition, int signId, Vector3 eggPos)
    {
        id = signId;

        position = new float[3];
        position[0] = checkPointPosition.x;
        position[1] = checkPointPosition.y;
        position[2] = checkPointPosition.z;

        eggPosition = new float[3];
        eggPosition[0] = eggPos.x;
        eggPosition[1] = eggPos.y;
        eggPosition[2] = eggPos.z;

        stars = GameManager.Instance.starsId;
        points = GameManager.Instance.Points;
    }
}
