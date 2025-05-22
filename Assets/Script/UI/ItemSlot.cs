using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;

    public Inventory inventory;


    private Button button;
    public Image icon;
    private TextMeshProUGUI quatityText;
    private Outline outline;

    public int index; //�ٵ� �ڱ� ��ġ�� �� �ʿ䰡 ������?
    public bool equipped;
    public int quantity;


    private void Awake()
    {
        outline = GetComponent<Outline>();
        button = GetComponent<Button>();
       
        quatityText = GetComponentInChildren<TextMeshProUGUI>();

    }
    private void OnEnable()
    {
        outline.enabled = equipped;
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quatityText.text = quantity > 1 ? quantity.ToString() : string.Empty;
        if (outline != null)
        {

            outline.enabled = equipped;
        }
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quatityText.text = string.Empty;
    }
    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }

}
