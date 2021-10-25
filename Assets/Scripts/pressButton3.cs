using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressButton3 : MonoBehaviour
{
    [SerializeField] Transform wall1;
    [SerializeField] Transform wall2;
    [SerializeField] Renderer button;

    [SerializeField] Transform cam;

    [SerializeField] LayerMask buttonMask3;

    [SerializeField] Material buttonMaterialUnpressed;
    [SerializeField] Material buttonMaterialPressed;

    private bool isPressed = false;
    private bool isClosing = false;


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
        if (Physics.Raycast(cam.position, cam.forward, out hit, 25f, buttonMask3) && !isPressed)
        {
            ButtonPress();
        }
    }

    void ButtonPress()
    {
        button.material = buttonMaterialPressed;
        isPressed = true;
        isClosing = false;
    }

    void UpdateWall()
    {
        print(wall2.position.z);
        if (isPressed && !isClosing && wall1.position.z < 331.6)
        {
            wall1.position += new Vector3(0f, 0f, 60f * Time.deltaTime);
        }
        if (isPressed && !isClosing && wall2.position.z > 125.3)
        {
            wall2.position -= new Vector3(0f, 0f, 60f * Time.deltaTime);
        }
        if (isPressed && wall1.position.z >= 331.6 && wall2.position.z <= 125.3)
        {
            button.material = buttonMaterialUnpressed;
            isPressed = false;
            isClosing = true;
        }

        if(isClosing)
        {
            if (!isPressed && isClosing && wall1.position.z > 270.5f)
            {
                wall1.position -= new Vector3(0f, 0f, 1.2f * Time.deltaTime);
            }
            if (!isPressed && isClosing && wall2.position.z < 184.3f)
            {
                wall2.position += new Vector3(0f, 0f, 1.2f * Time.deltaTime);
            }
        }
    }
}
