using System.Runtime.CompilerServices;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MiningRigManager : MonoBehaviour
{
    [Header("채굴장 필드")]
    [SerializeField] private Tilemap MiningField;
    [SerializeField] private GameObject[] expandFields;

    [Header("업그레이드 현황")]
    [SerializeField] private int expandCount = 0;
    [SerializeField] private int upgradeCount = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ExpandField();
        }
    }
    public void ExpandField()
    {
        expandCount++;

        expandFields[expandCount].SetActive(true);
    }

    public void UpgradeMiningRig()
    {
        
    }
}
