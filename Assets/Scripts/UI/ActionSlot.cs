using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ActionSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private CustomizationManager customizationManager;
    [SerializeField]
    private Image border;
    [SerializeField]
    private TextMeshProUGUI tmpro;
    [SerializeField]
    private ActionsSO actionSO;

    public void OnPointerClick(PointerEventData eventData)
    {
        border.gameObject.SetActive(true);

        customizationManager.Selected(this);
    }

    public void Deselect()
    {
        border.gameObject.SetActive(false);
    }

    public void Fill(ActionsSO action)
    {
        actionSO = action;

        tmpro.text = action.actionName;
    }

    public ActionsSO GetAction()
    {
        return actionSO;
    }
}
