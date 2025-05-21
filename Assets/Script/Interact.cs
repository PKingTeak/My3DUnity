using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    private float checkRate = 0.05f;
    [SerializeField]
    private float lastCheckTime;
    [SerializeField]
    private float maxCheckDistance;
    [SerializeField]
    private LayerMask layer;


    public GameObject curInteractObject;
    public IInteractable curInteractable;


    public TextMeshProUGUI promptText;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }


    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layer))
            {
                if (hit.collider.gameObject != curInteractObject)
                {
                    curInteractObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();

                }
            }
            else
            {
                curInteractObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }

    }

    private void SetPromptText()
    {

        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractInfo();
        
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        curInteractable.Oninteract();
        curInteractObject = null;
        curInteractable = null;
        promptText.gameObject.SetActive(false);

    }

}
