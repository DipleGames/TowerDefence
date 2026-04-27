using UnityEngine;

public enum MonsterType { Monster, Boss }

/// <summary>
/// 디자인 패턴 : 팩토리 패턴
/// </summary>
public class MonsterFactory : MonoBehaviour
{
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] GameObject bossPrefab;

    public GameObject Create(MonsterType type, Vector3 pos)
    {
        GameObject prefab = type switch
        {
            MonsterType.Monster => monsterPrefab,
            MonsterType.Boss => bossPrefab,
            _ => null
        };

        return Instantiate(prefab, pos, Quaternion.Euler(0f,-90f,0f));
    }
}