using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundlayer;
    [SerializeField]
    private float moveSpeed;
    private Vector2 curMoveMentInput; //방향 입력

    [SerializeField]
    private float jumpForce;
    private Vector2 mouseDelta;

    [Header("Camera")]
    [SerializeField]
    private Transform cameraContainer;
    private float minLookX = -90f;
    private float maxLookX = 90f;
    private float curCamRotX;
    public float lookSensitivity; //마우스 감도조절

    Rigidbody rigid;




    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        MoveMent();
    }
    private void LateUpdate()
    {
        CameraLook();
        Debug.DrawRay(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down);

    }



    void MoveMent()
    {

        Vector3 moveDir = transform.forward * curMoveMentInput.y + transform.right * curMoveMentInput.x;
        moveDir *= moveSpeed;
        moveDir.y = rigid.velocity.y;


        rigid.velocity = moveDir;

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {

            curMoveMentInput = context.ReadValue<Vector2>();

        }
        else
        {

            curMoveMentInput = Vector2.zero; //안움직임
        }

    }


    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>(); //마우스 이동량
    }

    private void CameraLook()
    {
        curCamRotX += mouseDelta.y * lookSensitivity;
       
        curCamRotX = Mathf.Clamp(curCamRotX,minLookX,maxLookX); //범위 제한
        cameraContainer.localEulerAngles = new Vector3(-curCamRotX, 0, 0);
        

        transform.eulerAngles += new Vector3(0,mouseDelta.x * lookSensitivity, 0); //카메라 회전

    }


    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {

            if (IsGrounded()) //여기에 추가로 버프를 먹으면 
            { 
           rigid.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            }
          
        }
    }


 

    private bool IsGrounded()
    {
        Ray[] ray = new Ray[4]
        {
          new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f)+(transform.up*0.01f),Vector3.down),
            new Ray(transform.position + (transform.right* 0.2f) +(transform.up* 0.01f) ,Vector3.down),
            new Ray(transform.position + (-transform.right* 0.2f) +(transform.up* 0.01f) ,Vector3.down)


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
