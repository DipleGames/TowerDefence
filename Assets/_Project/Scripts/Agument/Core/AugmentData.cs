using UnityEngine;
using System.Collections;

public abstract class AugmentData : ScriptableObject
{
    public string augmentName;
    [TextArea] public string augmentDesc;

    public abstract IEnumerator Execute();
}