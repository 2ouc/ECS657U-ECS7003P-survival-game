using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SpawnPointsGetter : MonoBehaviour
{
    public float minRange;
    public float maxRange;

    public List<spawnPointsData> PointsData = new List<spawnPointsData>();


    Transform spawnPoints, playerTransform;

    public static SpawnPointsGetter instance;
    // Start is called before the first frame update
    private void OnEnable()
    {
        instance = this;
    }
    void Start()
    {
        spawnPoints = transform;

        playerTransform = GameManager.instance.Player;

        for (int i = 0; i < spawnPoints.childCount; i++)
        {
            PointsData.Add(new spawnPointsData(Vector3.Distance(playerTransform.position, spawnPoints.GetChild(i).position),
                spawnPoints.GetChild(i).position.x,
                spawnPoints.GetChild(i).position.y,
                spawnPoints.GetChild(i).position.z));
        }

        PointsData.Sort(SortByDistance);
    }
    static int SortByDistance(spawnPointsData p1, spawnPointsData p2)
    {
        return p1.distance.CompareTo(p2.distance);
    }

    public Vector3[] GetPoints(int points,float min = 6, float max = 20)
    {
        int flag = 0;

        Vector3[] tempArrayToReturn = new Vector3[points];

        for (int i = 0; i < PointsData.Count; i++)
        {
            Vector3 tempVector = new Vector3(PointsData[i].x, PointsData[i].y, PointsData[i].z);
            float dist =  Vector3.Distance(playerTransform.position, tempVector);
            if (dist > min && dist < max)
            {
                tempArrayToReturn[flag].x = PointsData[i].x;
                tempArrayToReturn[flag].y = PointsData[i].y;
                tempArrayToReturn[flag].z = PointsData[i].z;
                flag++;

                //Debug.Log(flag);
                if (flag == points)
                {
                    return tempArrayToReturn;
                }
            }
        }
        return tempArrayToReturn;
    }
}
[System.Serializable]
public class spawnPointsData
{
    public float distance;
    public float x,y,z;
    public spawnPointsData(float distance, float x, float y, float z)
    {
        this.distance = distance;
        this.x = x;
        this.y = y;
        this.z = z;
    }
}