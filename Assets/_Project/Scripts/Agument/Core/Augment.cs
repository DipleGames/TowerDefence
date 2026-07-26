using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Augment : MonoBehaviour
{
    public Button button;
    public TMP_Text nameText;
    public TMP_Text descText;

    public AugmentData augment;

    [SerializeField] private Image glow;
    private Color color;
    private bool isGlow;

    void Awake()
    {
        color = glow.color;
    }

    void Update()
    {
        if (!isGlow)
            return;

        color.a = Mathf.PingPong(Time.unscaledTime * 2f, 1f);
        glow.color = color;
    }

    public void SetUnique(bool value)
    {
        isGlow = value;
        glow.gameObject.SetActive(value);
    }

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
