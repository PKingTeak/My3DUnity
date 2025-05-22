using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Buff,
    Consumable,
    Resource
}
public enum ItemMesh
{
    None,
    Sphere,
    Square
}


public enum BuffType
{
   Speed,
   Jump
}
[System.Serializable]
public class ItemDataConsumable
{
    public BuffType type;
    public float value;
};

[CreateAssetMenu(fileName = "Items", menuName = "ScriptableObject/Items")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string ItemName;
    public string ItemInfo;
    public int value;
    public ItemType type;
    public BuffType buffType;
    public Sprite icon;
    public GameObject dropPrefabs;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Mesh Change")]
    public ItemMesh meshType = ItemMesh.None;
    public Mesh applyMesh;








}
