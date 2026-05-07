using System.Collections;
using UnityEngine;

public abstract class CoreParts : ScriptableObject
{
    [Header("카드 정보")]
    public int coreID;
    public string coreName;
    public string coreDesc;
    public Sprite coreImg;

    public abstract IEnumerator EquipCorePart();
}
