using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressButton2 : MonoBehaviour
{
    [SerializeField] Transform platform1;
    [SerializeField] Transform platform2;
    [SerializeField] Transform platform3;
    [SerializeField] Renderer button;

    [SerializeField] Transform cam;

    [SerializeField] LayerMask buttonMask;

    [SerializeField] Material buttonMaterialUnpressed;
    [SerializeField] Material buttonMaterialPressed;

    private bool isPressed = false;
    private bool isFalling = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckPressButton();
        }
        UpdatePlatforms();
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
        isPressed = true;
        isFalling = false;
    }

    void UpdatePlatforms()
    {
        if (isPressed && !isFalling && platform1.position.y < 1170f)
        {
            platform1.position += new Vector3(0f, 40f * Time.deltaTime, 0f);
        }
        if (isPressed && !isFalling && platform2.position.y < 1225f)
        {
            platform2.position += new Vector3(0f, 40f * Time.deltaTime, 0f);
        }
        if (isPressed && !isFalling && platform3.position.y > 1330f)
        {
            platform3.position -= new Vector3(0f, 40f * Time.deltaTime, 0f);
        }

        if (isPressed && platform1.position.y >= 1170f && platform2.position.y >= 1225f && platform3.position.y <= 1330f)
        {
            button.material = buttonMaterialUnpressed;
            isPressed = false;
            isFalling = true;
            print("hit");
        }

        if(isFalling)
        {
            if (!isPressed && isFalling && platform1.position.y > 1125)
            {
                platform1.position -= new Vector3(0f, 2f * Time.deltaTime, 0f);
            }
            if (!isPressed && isFalling && platform2.position.y > 1150)
            {
                platform2.position -= new Vector3(0f, 2f * Time.deltaTime, 0f);
            }
            if (!isPressed && isFalling && platform3.position.y < 1410)
            {
                platform3.position += new Vector3(0f, 2f * Time.deltaTime, 0f);
            }
        }
    }
}
