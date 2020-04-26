using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    LevelData data;

    // Start is called before the first frame update
    void Start()
    {
        data = SaveSystem.LoadLevelData();

        // get the stars gameobjects from each level that is done
        for (int i=0; i<data.lastLevel; i++)
        {
            var level = this.transform.GetChild(i).transform.GetChild(1);

            for(int j=1; j<=data.stars[i+1]; j++)
            {
                var star = level.transform.GetChild(j-1);
                star.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            }
        }
    }

    public void StartLevel(int k)
    {
        if (k <= data.lastLevel)
            UIManager.Instance.StartLevel(k);
        else
            GameManager.Instance.UpdateInstructionsText("Level locked");
    }
}
