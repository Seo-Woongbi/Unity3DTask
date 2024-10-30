using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float startValue;
    public float curValue;
    public float maxValue;
    public float decreaseValue;
    public Image conditionBar;

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        conditionBar.fillAmount = GetPercentage();
    }

    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0f);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
