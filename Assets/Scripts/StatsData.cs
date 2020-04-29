using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsData
{
    // slowest&fastest win time
    public int eggDrop, killed;

    public StatsData()
    {
        eggDrop = 0;
        killed = 0;
    }

    public void IncreaseVal(int eggDropVal, int killedVal)
    {
        eggDrop += eggDropVal;
        killed += killedVal;
    }
}
