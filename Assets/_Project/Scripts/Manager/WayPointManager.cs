using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : SingleTon<WayPointManager>
{
    public List<Transform> wayPoints = new List<Transform>();
}
