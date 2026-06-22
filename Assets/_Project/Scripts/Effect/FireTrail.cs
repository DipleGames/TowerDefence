using System.Collections.Generic;
using UnityEngine;

public class FireTrail : MonoBehaviour
{
    private Dictionary<MonsterController, float> stayTimeDic = new();

    void OnTriggerStay(Collider other)
    {
        MonsterController monster = other.GetComponentInParent<MonsterController>();

        if (monster == null)
            return;

        if (!stayTimeDic.ContainsKey(monster))
            stayTimeDic.Add(monster, 0f);

        stayTimeDic[monster] += Time.deltaTime;

        if (stayTimeDic[monster] >= 0.5f)
        {
            Debug.Log("도트뎀");
            // monster.TakeDamage(dotDamage);

            stayTimeDic[monster] = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        MonsterController monster = other.GetComponentInParent<MonsterController>();

        if (monster != null)
            stayTimeDic.Remove(monster);
    }
}