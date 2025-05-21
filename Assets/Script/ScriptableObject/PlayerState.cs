using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerState", menuName = "ScriptableObject/PlayerState")]
public class PlayerState : ScriptableObject
{
    public int hp;
    
    public Mesh curPlayerMesh;
    // 다른 추가 상태
    public void Damaged(int value)
    {
        //땅에 떨어졌을때 -
        //아이템 먹었을때 +
        //UI는 그냥 Hp값을 가지고 그대로 화면에 표시만 해줄꺼임
        //데이터 처리는 이곳에서 하고 실제 화면에 표시되는UI는 값만 받아서 출력해줄예정
        hp += value;
        HealthChange?.Invoke();
    }
    public  Action HealthChange;

   
    
    
    //여기서 데미지 관련 함수를 만든다.
}
