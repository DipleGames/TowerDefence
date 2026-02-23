using UnityEngine;
using UnityEngine.UI;

public class Augment : MonoBehaviour
{
    public Button button;
    public Text nameText;
    public Text descText;

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
