using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Augment : MonoBehaviour
{
    public Button button;
    public TMP_Text nameText;
    public TMP_Text descText;

    public AugmentData augment;

    public void Bind(AugmentData augment)
    {
        this.augment = augment;

        nameText.text = augment.augmentName;
        descText.text = augment.augmentDesc;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        AugmentManager.Instance.ApplyAugment(augment);
    }
}
