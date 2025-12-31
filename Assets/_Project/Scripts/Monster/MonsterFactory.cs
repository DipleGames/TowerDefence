using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public enum MonsterType { Monster }

/// <summary>
/// 디자인 패턴 : 팩토리 패턴
/// </summary>
public class MonsterFactory : MonoBehaviour
{
    [SerializeField] GameObject monsterPrefab;

    public GameObject Create(MonsterType type, Vector3 pos)
    {
        GameObject prefab = type switch
        {
            MonsterType.Monster => monsterPrefab,
            _ => null
        };

        return Instantiate(prefab, pos, Quaternion.identity);
    }
}