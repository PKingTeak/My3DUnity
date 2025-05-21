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
    // �ٸ� �߰� ����
    public void Damaged(int value)
    {
        //���� ���������� -
        //������ �Ծ����� +
        //UI�� �׳� Hp���� ������ �״�� ȭ�鿡 ǥ�ø� ���ٲ���
        //������ ó���� �̰����� �ϰ� ���� ȭ�鿡 ǥ�õǴ�UI�� ���� �޾Ƽ� ������ٿ���
        hp += value;
        HealthChange?.Invoke();
    }
    public  Action HealthChange;

   
    
    
    //���⼭ ������ ���� �Լ��� �����.
}
