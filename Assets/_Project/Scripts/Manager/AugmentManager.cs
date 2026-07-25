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
        GameManager.Instance.SetPause(true);

        yield return new WaitUntil(() => isAugmentSelected);

        ViewManager.Instance.augmentView.SwithcAugmentUI();
        GameManager.Instance.SetPause(false);
    }

    public void RollAugments()
    {
        currentChoices.Clear();

        // 현재 등장 가능한 증강만 따로 모음
        List<AugmentData> availableAugments = new List<AugmentData>();

        foreach (AugmentData augment in allAugments)
        {
            // 유니크 증강인데 이미 1번 이상 획득했다면 제외
            if (augment.IsUnique && augment.Count >= 1)
                continue;

            availableAugments.Add(augment);
        }

        // 후보를 섞어서 최대 3개 선택
        while (currentChoices.Count < 3 && availableAugments.Count > 0)
        {
            int randomIndex = Random.Range(0, availableAugments.Count);

            currentChoices.Add(availableAugments[randomIndex]);
            availableAugments.RemoveAt(randomIndex);
        }

        // 버튼 전체 비활성화
        foreach (GameObject augmentBtn in augmentBtns)
        {
            augmentBtn.SetActive(false);
        }

        // 실제 선택된 증강 수만큼 버튼 활성화
        for (int i = 0; i < currentChoices.Count; i++)
        {
            augmentBtns[i].SetActive(true);
            augmentBtns[i].GetComponent<Augment>().Bind(currentChoices[i]);
        }
    }

    public void ApplyAugment(AugmentData augment)
    {
        if (isAugmentSelected) return;

        isAugmentSelected = true;
        augment.Count++;
        StartCoroutine(augment.Execute());
    }
}