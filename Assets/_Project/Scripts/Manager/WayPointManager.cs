using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    public static List<Transform> wayPoints = new List<Transform>();

    void Awake()
    {
        for(int i=0; i<8; i++)
        {
            wayPoints.Add(gameObject.transform.GetChild(i).transform);
        }
    }
}
