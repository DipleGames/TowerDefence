using UnityEngine;
using System.Collections;

public abstract class AugmentData : ScriptableObject
{
    public string augmentName;
    [TextArea] public string augmentDesc;
    public abstract bool IsUnique { get; }
    public abstract int Count {get;  set;}

    public abstract IEnumerator Execute();
}