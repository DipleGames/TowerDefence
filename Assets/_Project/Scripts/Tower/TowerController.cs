using Unity.VisualScripting;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public TowerModel towerModel;
    public SphereCollider detectSensor;

    void Awake()
    {
        detectSensor.radius = towerModel.detectRange;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            MonsterController monsterController = other.GetComponentInParent<MonsterController>();
            towerModel.monsters.Add(monsterController);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            MonsterController monsterController = other.GetComponentInParent<MonsterController>();
            towerModel.monsters.Remove(monsterController);
        }
    }

}
