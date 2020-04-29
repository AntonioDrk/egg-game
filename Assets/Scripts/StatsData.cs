using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsData
{
    public int eggDrop, deathByLaser, deathByFalling;
    public float slowestWin, fastestWin;

    public StatsData()
    {
        eggDrop = 0;
        deathByLaser = 0;
        deathByFalling = 0;
        slowestWin = 99999;
        fastestWin = 0;
    }

    public void IncreaseVal(int eggDropVal, int deathByLaserVal, int deathByFallingVal, float currentTime)
    {
        eggDrop += eggDropVal;
        deathByLaser += deathByLaserVal;
        deathByFalling += deathByFallingVal;

        if(currentTime > 0)
        {
            if (currentTime < slowestWin)
                slowestWin = currentTime;
            if (currentTime > fastestWin)
                fastestWin = currentTime;
        }
    }
}
