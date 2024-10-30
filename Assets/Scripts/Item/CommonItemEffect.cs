using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonItemEffect : MonoBehaviour
{
    PlayerCondition condition;   

    private void Start()
    {
        condition = GetComponent<PlayerCondition>();
    }

    public void UseItem(ItemData data)
    {
        switch (data.consumables.type)
        {
            case ConsumableType.Health:
                condition.Heal(data.consumables.value);
                break;
            case ConsumableType.Hunger:
                condition.Eat(data.consumables.value);
                break;
        }
    }
}
