using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int AchievementNum = 0;
        for (int i = 1; i < 100; i++)
        {
            var achievement = Resources.Load("Prefabs/Achievements/Achievement " + i) as GameObject;

            if (achievement != null)
            {
                AchievementNum++;
            }
            else
            {
                break;
            }

        }

        for (int i = 1; i <= AchievementNum; i++)
        {
            var achievement = Instantiate(Resources.Load("Prefabs/Achievements/Achievement " + i)) as GameObject;

            if (achievement != null)
            {
               
                achievement.transform.SetParent(gameObject.transform, false);
            }

        }
        for (int i = 0; i <= 20; i++)
        {
            var locked = Instantiate(Resources.Load("Prefabs/Achievements/Locked")) as GameObject;

            if (locked != null)
            {

                locked.transform.SetParent(gameObject.transform, false);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
