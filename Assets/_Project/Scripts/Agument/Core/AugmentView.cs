using UnityEngine;

public class AugmentView : MonoBehaviour
{
    public GameObject augmentPanel;

    public void SwithcAugmentUI()
    {
        augmentPanel.SetActive(!augmentPanel.activeSelf);
    }
}
