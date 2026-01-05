using System;
using System.Net.NetworkInformation;
using UnityEngine;

public class MainHeart : MonoBehaviour
{
    [SerializeField] private int _maxHP;
    [SerializeField] private int _currentHP;
    public int CurrentHP
    {
        get => _currentHP;
        set
        {
            int nv = value;
            Debug.Log(nv);
            _currentHP = nv;
            if(CurrentHP <= 0)
            {
                Debug.Log("심장이 파괴되었습니다 게임이 종료됩니다.");
                Destroy(gameObject);
                Time.timeScale = 0f;
            }
        }
    }

    void Start()
    {
        CurrentHP = _maxHP;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            Destroy(other.transform.parent.gameObject);
            CurrentHP -= 10;
        }
    }
}
