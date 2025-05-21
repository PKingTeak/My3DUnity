using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
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
    Sphere,
    Square
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
    public ItemType type;
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
