using System.Collections;
using UnityEngine;

public class AugmentView : MonoBehaviour
{
    public GameObject augmentPanel;
    public bool isAugmentUIActive = false;
    public GameObject[] augmentBtns;

    public void SwithcAugmentUI()
    {
        augmentPanel.SetActive(!augmentPanel.activeSelf);
        isAugmentUIActive = augmentPanel.activeSelf;
    }
}
