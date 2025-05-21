using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerState playerState { get => player; set => player = value; }
    [SerializeField]
    private PlayerState player;

    private PlayerController controller;


    private void Start()
    {
        controller = GetComponent<PlayerController>();
        
    }

    public void SetPlayer()
    {
        this.GetComponentInChildren<MeshFilter>().mesh = player.curPlayerMesh;
    }

 



    

    //체력 정보를 state
}
