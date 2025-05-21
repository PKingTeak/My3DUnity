using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Item : MonoBehaviour
{
    public float duration;
    [SerializeField]
    private PlayerState player;



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
