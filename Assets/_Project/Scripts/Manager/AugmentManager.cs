using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentManager : SingleTon<AugmentManager>
{
    [Header("모든 증강 후보")]
    public List<AugmentData> allAugments;

    [Header("이번 선택지")]
    public List<AugmentData> currentChoices = new List<AugmentData>();

    [Header("증가 카드 버튼")]
    public GameObject[] augmentBtns;

    public bool isAugmentSelected = false;

    public IEnumerator AugmentPhase(float maxDuration)
    {
        isAugmentSelected = false;
        RollAugments();
        ViewManager.Instance.augmentView.SwithcAugmentUI();

        float endTime = Time.time + maxDuration;

        yield return new WaitUntil(() => isAugmentSelected || Time.time >= endTime);
        ViewManager.Instance.augmentView.SwithcAugmentUI();

        yield break;
    }

    public void RollAugments()
    {
        currentChoices.Clear();

        List<int> used = new List<int>();

        while (currentChoices.Count < 3 && used.Count < allAugments.Count)
        {
            int ran = Random.Range(0, allAugments.Count);
            if (used.Contains(ran)) continue;

            used.Add(ran);
            currentChoices.Add(allAugments[ran]);
        }

        for(int i = 0; i<currentChoices.Count; i++)
        {
            augmentBtns[i].GetComponent<Augment>().Bind(currentChoices[i]);
        }
    }

    public void ApplyAugment(AugmentData augment)
    {
        if (isAugmentSelected) return;

        isAugmentSelected = true;
        StartCoroutine(augment.Execute());
    }
}