using UnityEngine;

public class MachineGridObject : MonoBehaviour
{
    [SerializeField] private FieldNode currentFieldNode;

    public FieldNode CurrentFieldNode => currentFieldNode;

    public void SetCurrentFieldNode(FieldNode node)
    {
        currentFieldNode = node;
    }
}