using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public interface IInteractable
{
    public string GetInteractInfo();
    public void Oninteract();
}
public class Item : MonoBehaviour, IInteractable
{

    public ItemData data;

    public float duration;


    private float buffTime = 1f;
    private float curTime;
    public string GetInteractInfo()
    {
        string str = $"{data.ItemName}\n{data.ItemInfo}";
        return str;

    }

    public void Oninteract()
    {
        GameManager.Instance.Player.itemdata = data;
        GameManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }




    private MeshFilter prePlayerMesh;
    private IEnumerator ChangeMeshBuff()
    {
        prePlayerMesh = GameManager.Instance.Player.PlayerMesh;
        GameManager.Instance.Player.PlayerMesh = this.GetComponent<MeshFilter>();
        yield return new WaitForSeconds(duration);
        GameManager.Instance.Player.PlayerMesh = prePlayerMesh;
    }

   
/*
    private void OnTriggerEnter(Collider other)
    {
        //테스트용
        
        StartCoroutine(ChangeMeshBuff());
        Destroy(this);
    }
 */

}
