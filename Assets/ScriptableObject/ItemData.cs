using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType
{
    Hunger,
    Health
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]

public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public GameObject dropPrefab;

    [Header("Consumable")]
    public ItemDataConsumable consumables;
}
