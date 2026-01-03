using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public enum GirdView { Main, Mining }
public class GridViewManager : MonoBehaviour
{
    public GirdView gridView = GirdView.Main;
    public void OnClickedViewChangeBtn()
    {
        switch(gridView)
        {
            case GirdView.Main:
                transform.position += new Vector3(50f, 0f, 0f);
                gridView = GirdView.Mining;
                HUDManager.Instance.OnChangeViewChangeBtnText(gridView);
                break;
            case GirdView.Mining:
                transform.position -= new Vector3(50f, 0f, 0f);
                gridView = GirdView.Main;
                HUDManager.Instance.OnChangeViewChangeBtnText(gridView);
                break;
        }
    }
}
