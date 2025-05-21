using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Items", menuName = "ScriptableObject/Items")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        Sphere,
        Square
    }

    public Mesh itemMesh;


    
}
