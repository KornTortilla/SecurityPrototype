using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationManager : MonoBehaviour
{
    [SerializeField]
    private Button fillButton;
    [SerializeField]
    private List<ActionSlot> currentSlots;

    private ActionSlot currentSelected;
    private ActionSlot poolSelected;

    public void Selected(ActionSlot slot)
    {
        if (currentSlots.Contains(slot))
        {
            if (currentSelected) currentSelected.Deselect();

            currentSelected = slot;
        }
        else
        {
            if (poolSelected) poolSelected.Deselect();

            poolSelected = slot;
        }

        if (currentSelected && poolSelected)
        {
            Debug.Log("Eeeee");

            fillButton.interactable = true;
        }
    }

    public void Fill()
    {
        currentSelected.Fill(poolSelected.GetAction());
    }

    public void SendActions()
    {
        List<ActionsSO> actions = new List<ActionsSO>();
        foreach(ActionSlot actionSlot in currentSlots)
        {
            actions.Add(actionSlot.GetAction());
        }

        ActionTransfer.instance.CopyActions(actions);
    }
}
