using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Action")]
public class ActionsSO : ScriptableObject
{
    public string actionName;

    public string animationTriggerName;

    public float cooldown = 1f;
}
