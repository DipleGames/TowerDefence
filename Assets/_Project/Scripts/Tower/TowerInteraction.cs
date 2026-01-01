using Mono.Cecil.Cil;
using UnityEngine;

public class TowerInteraction : MonoBehaviour
{
    public GameObject uiRoot;

    void OnMouseDown()
    {
        uiRoot.SetActive(!uiRoot.activeSelf);
    }

}
