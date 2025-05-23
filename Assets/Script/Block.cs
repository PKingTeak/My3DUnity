using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public enum BlockType
{
    DamageBlock,
    NormalBlock,
    JumpBlock,
    MoveBlock

}

public enum MoveType
{

    horizontal,
    vertical,
    None

}
public class Block : MonoBehaviour
{


    [SerializeField]
    BlockType type;

    [SerializeField]
    private int Damage;
    PlayerState state;

    [Header("MoveBlock")]
    [SerializeField] private MoveType moveType;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxDistance;
    [SerializeField] private float minDistance;

    private bool isMax = false;
    [SerializeField] List<Transform> connectObjects = new List<Transform>();



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            state = player.playerState;
            InteractBlock(player);
            if (type == BlockType.MoveBlock)
            {
                collision.transform.SetParent(this.transform);
                //connectObjects.Add(collision.transform);
                //해당하는 오브젝트들 추가
            }
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            StopAllCoroutines();


        }
        collision.transform.SetParent(null);
        //시간이 없어서 일단 플레이어만 적용 
        //주말에 혼자서 해봐야겠다.
        //connectObjects.Remove(collision.transform);
        //해당하는 오브젝트들 제거
    }

    public void InteractBlock(Player player)
    {
        switch (type)
        {
            case BlockType.DamageBlock:
                StartCoroutine(DurationDamage());
                break;
            case BlockType.NormalBlock:
                break;
            case BlockType.JumpBlock:
                StartCoroutine(UPJumpForce(player.transform));
                break;


        }
    }

    private IEnumerator DurationDamage()
    {
        while (true)
        {
            state.hp -= 5;
            yield return new WaitForSeconds(1f);
        }

        //1�ʸ��� ������ �ֱ� 


    }

    private IEnumerator UPJumpForce(Transform player)
    {
        player.TryGetComponent<Rigidbody>(out Rigidbody rigid);
        {

            rigid.AddForce(Vector2.up * 10, ForceMode.Impulse);
        }

        yield return null;
    }


    private void Update()
    {
        CheckDistance();
        MoveBlock();
        MoveWithBlock();
    }

    private void MoveWithBlock()
    {
        if (connectObjects.Count > 0)
        {
            foreach (var obj in connectObjects)
            {
                obj.transform.position += transform.position * moveSpeed * Time.deltaTime;
                //같다라고 하는거랑 무슨차이인가? 
                //아이에 같다고 지정해 버리면 가운데에 고정되어서 움직이게 된다. 
            }
        }
    }

    private void MoveBlock()
    {

        if (moveType == MoveType.vertical)
        {
            if (isMax)
            {
                transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            }

        }
        else if(moveType == MoveType.horizontal)
        {
            if (isMax)
            {
                transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            }

        }
        
    }
    private void CheckDistance()
    {
        if (moveType == MoveType.vertical)
        {


            if (transform.position.y > maxDistance)
            {
                isMax = true;
            }
            else if (transform.position.y < minDistance)
            {
                isMax = false;

            }
        }
        else if (moveType == MoveType.horizontal)
        {
            if (transform.position.x > maxDistance)
            {
                isMax = true;
            }
            
            else if (transform.position.x < minDistance)
            {
                isMax = false;

            }
        }
    }
}




   


