using System.Runtime.CompilerServices;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MiningRigManager : MonoBehaviour
{
    [Header("채굴장 필드")]
    [SerializeField] private Tilemap miningField;
    [SerializeField] private Tilemap miningRigsFloor;
    [SerializeField] private GameObject[] expandFields;

    [Header("채굴기 원본")]
    [SerializeField] private GameObject miningRig;


    [Header("업그레이드 현황")]
    [SerializeField] private int expandCount = 0;
    [SerializeField] private int additionCount = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ExpandField();
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            AddOnMiningRig();
        }
    }
    public void ExpandField()
    {
        expandCount++;

        expandFields[expandCount].SetActive(true);
    }

    public void AddOnMiningRig()
    {
        additionCount++;

        Transform parent = miningRigsFloor.transform;
        if (additionCount >= parent.childCount) return;

        GameObject mr = parent.GetChild(additionCount).gameObject;
        if (mr == null) return;

        // 기준 Animator
        Animator master = miningRig.GetComponentInChildren<Animator>();
        Animator target = mr.GetComponentInChildren<Animator>();

        // 컨트롤러 복사
        target.runtimeAnimatorController = master.runtimeAnimatorController;

        // 오브젝트 먼저 켜서 Animator가 활성화되게
        mr.SetActive(true);

        // 같은 상태/같은 타이밍으로 맞추기
        var masterState = master.GetCurrentAnimatorStateInfo(0);
        int stateHash = masterState.fullPathHash;

        float t = masterState.normalizedTime % 1f;   // 루프면 0~1로 맞추기
        target.Play(stateHash, 0, t);
        target.Update(0f); // 즉시 반영
    }

}
