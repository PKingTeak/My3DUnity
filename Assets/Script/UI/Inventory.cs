using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private BuffUIManager buffUIManager;

    private ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;

    [Header("SelectItem")]
    private ItemSlot selectItem;
    private int selectItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;


    public GameObject useButton;

    private PlayerController controller;
   

    private void Start()
    {
       
        controller = GameManager.Instance.Player.Controller;
        buffUIManager = FindObjectOfType<BuffUIManager>(); // 버프 관련 UI;

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
        ItemData data = GameManager.Instance.Player.itemdata;//�̰� ������
        if (data.canStack)
        {
        //�̹� �����Ͱ� �ִ� �����͸�
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.quantity++;
                UpdateUI();
                GameManager.Instance.Player.itemdata = null; //���� ����ֱ�
                return;

            }
        }

        ItemSlot emptySlot = GetEmptySlot();
        //����ִ� ������ ������
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
    {//���� ������ ���� ������ clear���ִ� �Լ�
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
        if (slots[index].item == null) return;//���� ������
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
                    controller.StartCoroutine(controller.ApplySpeedUP(selectItem.item.value, selectItem.item.duration)); //�����丵 ����
                    buffUIManager.AddBuffUI(selectItem.item);
                    break;
                case BuffType.Jump:
                    controller.ApplyJumpUP(selectItem.item.value, selectItem.item.duration);
                    buffUIManager.AddBuffUI(selectItem.item);
                    break;

            }
            RemoveSelctedItem();


        }

        //if(selctItem.item.type == ItemType.Weapon)
        //기타 등등 이렇게 추가하는게 맞는건가?
        
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
