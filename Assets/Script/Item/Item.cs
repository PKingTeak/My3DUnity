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
    [SerializeField]
    private PlayerState player;

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


    private float buffTime=1f;
    private float curTime;


    private Mesh prePlayerMesh;
    private IEnumerator ChangeMeshBuff()
    {
        prePlayerMesh = player.curPlayerMesh;
        player.curPlayerMesh = this.GetComponent<MeshFilter>().mesh;
        yield return new WaitForSeconds(duration);
        player.curPlayerMesh = prePlayerMesh;
    }

   

    private void OnTriggerEnter(Collider other)
    {
        //테스트용
        
        StartCoroutine(ChangeMeshBuff());
        Destroy(this);
    }

}
