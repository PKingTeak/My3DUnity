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

    public PlayerController Controller { get => controller;}

    [SerializeField]
    private Mesh playerMesh;
    public Mesh PlayerMesh { get => PlayerMesh; set => PlayerMesh = value; }

    public ItemData itemdata;
    public Action addItem;

    private void Awake()
    {
        GameManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        playerMesh = GetComponentInChildren<Mesh>();
    }

    public void SetPlayer()
    {
        this.GetComponentInChildren<MeshFilter>().mesh = player.curPlayerMesh;
    }

 



    

    //체력 정보를 state
}
