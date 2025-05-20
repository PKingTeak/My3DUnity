using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Vector2 curMovementDir; //방향 입력

    [SerializeField]
    private float jumpForce;

    [Header("Camera")]
    [SerializeField]
    private Transform cameraContainer;
    private float minLookX;
    private float maxLookX;
    private float curCamRotX;




    private Vector2 MouseDelta;//마우스 이동량


    private LayerMask groundlayer;


    Rigidbody rigid;


    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void MoveMent()
    {

        Vector3 moveDir = transform.forward * curMovementDir.y + transform.right * curMovementDir.x;
        moveDir *= moveSpeed * Time.deltaTime;
        moveDir.y = rigid.velocity.y;


        rigid.velocity = moveDir;

    }


//이거랑 같은거 아닌가요??아래
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {

            curMovementDir = context.ReadValue<Vector2>();

        }
        else
        {

            curMovementDir = Vector2.zero; //안움직임
        }

    }

    void JumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1.1f, groundlayer))
            {
                rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private bool IsGrounded()
    {
        Ray[] ray = new Ray[4]
        {
          new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-Vector3.forward * 0.2f)+(transform.up*0.01f),Vector3.down),
            new Ray(transform.position + (Vector3.right* 0.5f) +(transform.up* 0.01f) ,Vector3.down),
            new Ray(transform.position + (-Vector3.right* 0.5f) +(transform.up* 0.01f) ,Vector3.down)


        };

        for (int i = 0; i < ray.Length; i++)
        {
            if (Physics.Raycast(ray[i], 0.1f, groundlayer))
            {
                return true;

            }

        }

        return false;

    }  


}
