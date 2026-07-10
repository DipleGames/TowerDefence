using UnityEngine;
using System.Collections.Generic;

public class MonsterDatabase : SingleTon<MonsterDatabase>
{
    private Dictionary<int, MonsterData> _monsterDict = new();
    public int monsterCount = -1;

    protected override  void Awake()
    {

        LoadMonsterData();
    }

    public MonsterData GetMonsterData(int wave)
    {
        return _monsterDict[wave];
    }

    private void LoadMonsterData()
    {
        TextAsset csv = Resources.Load<TextAsset>("Data/MonsterDataTable");

        string[] lines = csv.text.Split('\n');

        // 첫 줄(Header) 제외
        for (int i = 2; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            string[] values = lines[i].Split(',');

            MonsterData data = new MonsterData();

            data.Wave = int.Parse(values[0]);
            data.ID = int.Parse(values[1]);
            data.HP = float.Parse(values[2]);
            data.Armor = float.Parse(values[3]);
            data.Speed = float.Parse(values[4]);
            data.Reward = int.Parse(values[5]);

            _monsterDict.Add(data.Wave, data);
        }
        monsterCount = _monsterDict.Count;
        Debug.Log($"Monster Loaded : {_monsterDict.Count}");
    }
}
