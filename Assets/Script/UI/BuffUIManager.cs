using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BuffUIManager : MonoBehaviour
{

    public BuffUI[] buffUIs;

    private void Start()
    {
        buffUIs = new BuffUI[this.transform.childCount];
        for (int i = 0; i < buffUIs.Length; i++)
        {
            buffUIs[i] = this.transform.GetChild(i).GetComponent<BuffUI>();
            buffUIs[i].gameObject.SetActive(false);
        }
    }

   

    public void AddBuffUI(ItemData item)
    {
        for (int i = 0; i < buffUIs.Length; i++)
        {
            if (buffUIs[i].icon.sprite == item.icon)
            {
                buffUIs[i].Useitem(item.duration, item.icon);
                return;
            }
        }
        
        int index = EmptyCheck();
        buffUIs[index].Useitem(item.duration, item.icon);

    }
    

    private int EmptyCheck()
    {
        for (int i = 0; i < buffUIs.Length; i++)
        {

            if (buffUIs[i].gameObject.activeSelf == false)
            {
                buffUIs[i].gameObject.SetActive(true);
                return i;
            }

        }
        return -1;

    }
}
