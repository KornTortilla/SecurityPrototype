using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionList : MonoBehaviour
{
    [SerializeField]
    private GameObject actionPrefab;
    private List<GameObject> actionObjectList;

    private void Awake()
    {
        actionObjectList = new List<GameObject>();
    }

    private void OnEnable()
    {
        ActionInput.newActionEvent += AddAction;
    }

    private void OnDisable()
    {
        ActionInput.newActionEvent -= AddAction;
    }

    private void AddAction(string name, float cooldown)
    {
        GameObject skillObject = Instantiate(actionPrefab, this.transform);
        skillObject.GetComponent<ActionImage>().Initialize(actionObjectList.Count, name, cooldown);
        actionObjectList.Add(skillObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveListUp();

            Scroll(true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject action = actionObjectList[actionObjectList.Count - 1];
            actionObjectList.Remove(action);
            actionObjectList.Insert(0, action);

            Scroll(false);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveListUp();

            StartCoroutine(actionObjectList[1].GetComponent<ActionImage>().Cooldown());

            Scroll(true);
        }
    }

    private void MoveListUp()
    {
        GameObject s = actionObjectList[0];
        actionObjectList.Remove(s);
        actionObjectList.Add(s);
    }

    private void Scroll(bool movingUp)
    {
        for (int i = 0; i < actionObjectList.Count; i++)
        {
            GameObject skillObject = actionObjectList[i];

            bool skip = false;
            if ((!movingUp && i == 0) || (movingUp && i == actionObjectList.Count - 1))
            {
                skip = true;
            }

            StartCoroutine(skillObject.GetComponent<ActionImage>().Move(skillObject, i, skip));
        }
    }
}
