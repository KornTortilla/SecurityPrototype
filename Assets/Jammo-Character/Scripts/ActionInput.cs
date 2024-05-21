using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionInput : MonoBehaviour
{
    public static Action<string, float> newActionEvent;

    [SerializeField]
    private CharacterSkinController skinController;

    [SerializeField]
    private ActionsSO[] tempActions;

    private List<ActionsSO> actions;
    private List<float> cooldowns;
    private int focusInt = 2;

    void Start()
    {
        actions = new List<ActionsSO>();
        cooldowns = new List<float>();
        List<ActionsSO> temp = ActionTransfer.instance.actions;
        for(int i = 0; i < temp.Count; i++)
        {
            actions.Add(temp[i]);
            newActionEvent?.Invoke(actions[i].actionName, actions[i].cooldown);

            cooldowns.Add(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            focusInt++;

            if (focusInt == actions.Count) focusInt = 0;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            focusInt--;

            if (focusInt < 0) focusInt = actions.Count - 1;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ActionsSO so = actions[focusInt];
            if(cooldowns[focusInt] == 1)
            {
                skinController.ChangeAnimatorIdle(so.animationTriggerName);
                StartCoroutine(Cooldown(focusInt, so.cooldown));

                focusInt++;

                if (focusInt == actions.Count) focusInt = 0;
            }
        }
    }

    private IEnumerator Cooldown(int position, float cooldown)
    {
        float timer = 0f;
        while(timer < cooldown)
        {
            cooldowns[position] = Mathf.Lerp(0f, 1f, timer / cooldown);

            timer += Time.deltaTime;

            yield return 0f;
        }

        cooldowns[position] = 1f;
    }
}
