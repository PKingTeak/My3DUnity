using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateUI : MonoBehaviour
{
    [SerializeField]
    private PlayerState state;

    private float maxHp = 100;
    private float curHp;
    public Image uiBar;
    private void Awake()
    {

        state.HealthChange += ShowHp;


    }



    private void OnDisable()
    {
        state.HealthChange -= ShowHp; //구독 해제
                                   //해제
    }

    public float Percent()
    { 
        curHp = state.hp;
        if (curHp < 0)
        {
            curHp = 0;
        }
        else if (curHp > maxHp)
        {
            curHp = maxHp;
        }
            return curHp / maxHp;
    }

    public void ShowHp()
    {
        uiBar.fillAmount =  Percent();


    }

    private void Update()
    {
        ShowHp();
    }

    //데이터만 가져와서 봐주고
}
