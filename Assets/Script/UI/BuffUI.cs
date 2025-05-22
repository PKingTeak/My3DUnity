using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;  

public class BuffUI : MonoBehaviour
{


    [SerializeField]
    private TextMeshProUGUI timeText;
    private float duration;

    private float curduration;
    public Image icon;
    public Image durationBar;
    public void Useitem(float _duration, Sprite _icon)
    {
        duration = _duration;
        curduration = _duration;
        durationBar.fillAmount = 1;
        icon.sprite = _icon;

    }

    private void ShowTime()
    {
        if (duration > 0)
        {
            timeText.text = curduration.ToString("F1"); //소수점1까지
            durationBar.fillAmount = curduration / duration;
        }
        else
        { 
        this.gameObject.SetActive(false);

        }
       

    }


    // Update is called once per frame
    void Update()
    {
        curduration -= Time.deltaTime;

        ShowTime();     
    }
}
