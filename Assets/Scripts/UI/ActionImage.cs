using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionImage : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI tmPro;


    [SerializeField]
    private float timeToMove = 0.2f;
    
    [SerializeField]
    private float verticalPadding = 100f;
    [SerializeField]
    private float focusPadding = 50f;

    private float cooldownTime = 5f;

    [HideInInspector]
    public bool onCooldown;

    public void Initialize(int position, string name, float cooldown)
    {
        rectTransform.anchoredPosition = new Vector2((position == 2) ? focusPadding : 0, (position - 2) * verticalPadding);
        tmPro.text = name;
        cooldownTime = cooldown;
    }

    public IEnumerator Move(GameObject skill, int position, bool skip)
    {
        Vector2 oldPos = rectTransform.anchoredPosition;
        float newY = (position - 2) * verticalPadding;

        Vector2 newPos = new Vector2(((position == 2) ? focusPadding : 0), newY);

        float timer = 0f;
        float time = (!skip) ? timeToMove : 0f;
        while (timer < time)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(oldPos, newPos, timer / time);

            timer += Time.deltaTime;

            yield return 0;
        }

        rectTransform.anchoredPosition = newPos;
    }

    public IEnumerator Cooldown()
    {
        onCooldown = true;

        float timer = 0f;
        while (timer < cooldownTime)
        {
            slider.value = Mathf.Lerp(0, 1, timer / cooldownTime);

            timer += Time.deltaTime;

            yield return 0;
        }

        slider.value = 1;
        onCooldown = false;
    }
}
