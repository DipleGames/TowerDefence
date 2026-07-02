using UnityEngine;
using System.Collections.Generic;

public class MonsterDatabase : SingleTon<MonsterDatabase>
{
    // private Dictionary<int, MonsterData> _monsterDict = new();

    // protected override  void Awake()
    // {

    //     LoadMonsterData();
    // }

    // public MonsterData GetMonsterData(int id)
    // {
    //     return _monsterDict[id];
    // }

    // private void LoadMonsterData()
    // {
    //     TextAsset csv = Resources.Load<TextAsset>("Data/MonsterData");

    //     string[] lines = csv.text.Split('\n');

    //     // 첫 줄(Header) 제외
    //     for (int i = 1; i < lines.Length; i++)
    //     {
    //         if (string.IsNullOrWhiteSpace(lines[i]))
    //             continue;

    //         string[] values = lines[i].Split(',');

    //         MonsterData data = new MonsterData();

    //         data.ID = int.Parse(values[0]);
    //         data.Name = values[1];
    //         data.HP = float.Parse(values[2]);
    //         data.Speed = float.Parse(values[3]);
    //         data.Attack = float.Parse(values[4]);
    //         data.Reward = int.Parse(values[5]);

    //         _monsterDict.Add(data.ID, data);
    //     }

    //     Debug.Log($"Monster Loaded : {_monsterDict.Count}");
    // }
}
