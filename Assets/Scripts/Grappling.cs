using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grappling : MonoBehaviour
{

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask groundMask;
    public LayerMask grappleMask;
    public LayerMask button1Mask;
    public LayerMask button2Mask;
    public LayerMask button3Mask;
    public Transform gunTip, cam, player;

    private SpringJoint joint;

    [SerializeField] Image crosshair = null;

    [SerializeField] AudioSource grappleAudio = null;

    public float maxDistance = 25f;

    public float reelSpeed = 5f;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
        CrosshairUpdate();

    }

    void LateUpdate()
    {
        DrawLine();

    }

    void CrosshairUpdate()
    {
        RaycastHit inRange;
        if (Physics.Raycast(cam.position, cam.forward, out inRange, maxDistance, groundMask))
        {
            crosshair.color = new Color(255, 255, 255);
        }
        else if (Physics.Raycast(cam.position, cam.forward, out inRange, maxDistance, grappleMask))
        {
            crosshair.color = new Color(255, 255, 255);
        }
        else if (Physics.Raycast(cam.position, cam.forward, out inRange, maxDistance, button1Mask))
        {
            crosshair.color = new Color(255, 255, 255);
        }
        else if (Physics.Raycast(cam.position, cam.forward, out inRange, maxDistance, button2Mask))
        {
            crosshair.color = new Color(255, 255, 255);
        }
        else if (Physics.Raycast(cam.position, cam.forward, out inRange, maxDistance, button3Mask))
        {
            crosshair.color = new Color(255, 255, 255);
        }
        else
        {
            crosshair.color = new Color(255, 0, 0);
        }

    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, groundMask))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            joint.enableCollision = false;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = 0f;

            joint.spring = 8f;
            joint.damper = 20f;
            joint.massScale = 1f;

            lr.positionCount = 2;
            PlaySound();
        }
        else if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, grappleMask))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            joint.enableCollision = false;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = 0f;

            joint.spring = 8f;
            joint.damper = 20f;
            joint.massScale = 1f;

            lr.positionCount = 2;
            PlaySound();
        }
        else if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, button1Mask))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            joint.enableCollision = false;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = 0f;

            joint.spring = 8f;
            joint.damper = 20f;
            joint.massScale = 1f;

            lr.positionCount = 2;
            PlaySound();
        }
        else if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, button2Mask))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            joint.enableCollision = false;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = 0f;

            joint.spring = 8f;
            joint.damper = 20f;
            joint.massScale = 1f;

            lr.positionCount = 2;
            PlaySound();
        }
        else if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, button3Mask))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            joint.enableCollision = false;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = 0f;

            joint.spring = 8f;
            joint.damper = 20f;
            joint.massScale = 1f;

            lr.positionCount = 2;
            PlaySound();
        }
    }

    void DrawLine()
    {
        if (joint)
        {
            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, grapplePoint);
        }
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);

    }

    void PlaySound()
    {
        grappleAudio.Play(0);
    }

}
