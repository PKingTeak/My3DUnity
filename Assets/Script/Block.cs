using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum BlockType
{
    DamageBlock,
    NormalBlock,
    JumpBlock,
    MoveBlock

}
public class Block : MonoBehaviour
{


    [SerializeField]
    BlockType type;

    [SerializeField]
    private int Damage;

    PlayerState state;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            state = player.playerState;
            InteractBlock(player);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            StopAllCoroutines();
        }
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

        //1초마다 데미지 주기 


    }

    private IEnumerator UPJumpForce(Transform player)
    {
        player.TryGetComponent<Rigidbody>(out Rigidbody rigid);
        {

            rigid.AddForce(Vector2.up * 10, ForceMode.Impulse);
        }

        yield return null;
    }



}
