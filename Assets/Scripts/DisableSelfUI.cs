using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class DisableSelfUI : MonoBehaviour
{
    private Selectable uiToDisable;

    private void Awake()
    {
        uiToDisable = GetComponent<Selectable>();
    }

    public void DisableUI()
    {
        uiToDisable.interactable = false;
    }
}
