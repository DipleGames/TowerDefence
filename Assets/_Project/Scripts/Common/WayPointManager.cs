using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    public static List<Transform> wayPoints = new List<Transform>();

    void Awake()
    {
        GameObject wayPointGroups = GameObject.Find("WayPointGroups");
        for(int i=0; i<6; i++)
        {
            wayPoints.Add(wayPointGroups.transform.GetChild(i).transform);
        }
    }
}
