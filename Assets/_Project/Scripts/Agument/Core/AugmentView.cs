using UnityEngine;

public class AugmentView : MonoBehaviour
{
    public GameObject augmentPanel;
    public bool isAugmentUIActive = false;

    public void SwithcAugmentUI()
    {
        augmentPanel.SetActive(!augmentPanel.activeSelf);
        isAugmentUIActive = augmentPanel.activeSelf;
    }
}
