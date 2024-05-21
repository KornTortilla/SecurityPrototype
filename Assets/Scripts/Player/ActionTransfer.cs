using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTransfer : MonoBehaviour
{
    public static ActionTransfer instance;

    public List<ActionsSO> actions;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            actions = new List<ActionsSO>();
        }
    }

    public void CopyActions(List<ActionsSO> actions)
    {
        this.actions = actions;

        Debug.Log(this.actions);
    }
}
