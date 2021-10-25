using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressButton : MonoBehaviour
{
    [SerializeField] Transform wall1;
    [SerializeField] Transform wall2;
    [SerializeField] Renderer button;
    [SerializeField] Renderer button2;

    [SerializeField] Transform cam;

    [SerializeField] LayerMask buttonMask;

    [SerializeField] Material buttonMaterialUnpressed;
    [SerializeField] Material buttonMaterialPressed;

    private bool isPressed = false;
    private bool isClosing = false;

    private float wall1Starting;
    private float wall2Starting;

    void Start()
    {
        float wall1Starting = wall1.position.z;
        float wall2Starting = wall2.position.z;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckPressButton();
        }
        UpdateWall();
    }

    void CheckPressButton()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 25f, buttonMask) && !isPressed)
        {
            ButtonPress();
        }
    }

    void ButtonPress()
    {
        button.material = buttonMaterialPressed;
        button2.material = buttonMaterialPressed;
        isPressed = true;
        isClosing = false;
    }

    void UpdateWall()
    {
        if (isPressed && !isClosing && wall1.position.z > 130f)
        {
            wall1.position -= new Vector3(0f, 0f, 100f * Time.deltaTime);
        }
        if (isPressed && !isClosing && wall2.position.z < 325f)
        {
            wall2.position += new Vector3(0f, 0f, 100f * Time.deltaTime);
        }
        if (isPressed && wall1.position.z <= 130f && wall2.position.z >= 325f)
        {
            button.material = buttonMaterialUnpressed;
            button2.material = buttonMaterialUnpressed;
            isPressed = false;
            isClosing = true;
            print("hit");
        }

        if(isClosing)
        {
            if (!isPressed && isClosing && wall1.position.z < 178f)
            {
                wall1.position += new Vector3(0f, 0f, 3f * Time.deltaTime);
            }
        if (!isPressed && isClosing && wall2.position.z > 277.5f)
            {
                wall2.position -= new Vector3(0f, 0f, 3f * Time.deltaTime);
            }  
        }
    }
}
