using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Items", menuName = "ScriptableObject/Items")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    { 
        Consumable,
        Resource
    }

    public enum ItemMesh
    {
        Sphere,
        Square
    }

    public Mesh itemMesh;

    public string ItemName;
    public string ItemInfo;


    
}
