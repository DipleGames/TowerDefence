using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentManager : SingleTon<AugmentManager>
{
    [Header("모든 증강 후보")]
    public List<AugmentData> allAugments;

    [Header("이번 선택지")]
    public List<AugmentData> currentChoices = new List<AugmentData>();

    [Header("증강 카드 버튼")]

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

        List<AugmentData> availableAugments = new List<AugmentData>();

        foreach (AugmentData augment in allAugments)
        {
            if (augment.IsUnique && augment.Count >= 1)
                continue;

            availableAugments.Add(augment);
        }

        while (currentChoices.Count < 3 && availableAugments.Count > 0)
        {
            int randomIndex = Random.Range(0, availableAugments.Count);

            currentChoices.Add(availableAugments[randomIndex]);
            availableAugments.RemoveAt(randomIndex);
        }

        GameObject[] augmentButtons = ViewManager.Instance.augmentView.augmentBtns;

        foreach (GameObject buttonObject in augmentButtons)
        {
            Augment augmentButton = buttonObject.GetComponent<Augment>();

            augmentButton.SetUnique(false);
            buttonObject.SetActive(false);
        }

        for (int i = 0; i < currentChoices.Count; i++)
        {
            GameObject buttonObject = augmentButtons[i];

            Augment augmentButton = buttonObject.GetComponent<Augment>();

            AugmentData augmentData = currentChoices[i];

            buttonObject.SetActive(true);
            augmentButton.Bind(augmentData);
            augmentButton.SetUnique(augmentData.IsUnique);
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