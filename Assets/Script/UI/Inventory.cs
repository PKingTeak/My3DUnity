using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;

    [Header("SelectItem")]
    private ItemSlot selectItem;
    private int selectItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;


    public GameObject useButton;
    public BuffUIManager buffManager;

    private PlayerController controller;
   

    private void Start()
    {
       
        controller = GameManager.Instance.Player.Controller;

        controller.inventory += Toggle;

        GameManager.Instance.Player.addItem += AddItem;

        inventoryWindow.gameObject.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
            slots[i].Clear();
            
        }
    }

   

    public void Toggle()
    {
        if (IsOpen())
        {
            inventoryWindow.SetActive(false);

        }
        else 
        {
            inventoryWindow.SetActive(true);
        }

    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy; 
    }

    public void AddItem()
    {
        ItemData data = GameManager.Instance.Player.itemdata;//이걸 넣을꺼
        if (data.canStack)
        {
        //이미 데이터가 있는 데이터면
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.quantity++;
                UpdateUI();
                GameManager.Instance.Player.itemdata = null; //값을 비워주기
                return;

            }
        }

        ItemSlot emptySlot = GetEmptySlot();
        //비어있는 슬롯이 없으면
        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            GameManager.Instance.Player.itemdata = null;
            return;
        }

        ClearSelectWindow();
    }

    void ClearSelectWindow()
    {
        selectItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        useButton.SetActive(false);
    }

    private ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
            
        }
        return null;
    }
    public void UpdateUI()
    {//값이 있으면 갱신 없으면 clear해주는 함수
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }


    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;//없는 아이템
        selectItem = slots[index];

        selectedItemName.text = selectItem.item.ItemName;
        selectedItemDescription.text = selectItem.item.ItemInfo;


        useButton.SetActive(selectItem.item.type == ItemType.Buff);
    }

    public void OnUseButton()
    {
        if (selectItem.item.type == ItemType.Buff)
        {
            switch (selectItem.item.buffType)
            {
                case BuffType.Speed:
                    controller.StartCoroutine(controller.ApplySpeedUP(selectItem.item.value,selectItem.item.duration)); //리펙토링 예정
                    buffManager.AddBuffUI(selectItem.item);
                    break;
                case BuffType.Jump:
                    controller.StartCoroutine(controller.ApplyJumpUP(selectItem.item.value, selectItem.item.duration));
                    buffManager.AddBuffUI(selectItem.item);
                    break;
                case BuffType.DoubleJump:
                    controller.StartCoroutine(controller.ApplyDoubleJump(selectItem.item.duration));
                    buffManager.AddBuffUI(selectItem.item);
                    break;
               

            }
            RemoveSelctedItem();


        }
    }


    void RemoveSelctedItem()
    {
        selectItem.quantity--;

        if (selectItem.quantity <= 0)
        {
            selectItem.item = null;
            ClearSelectWindow();
        }

        UpdateUI();
    }



}
