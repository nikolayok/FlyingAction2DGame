using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject borders;
    private Transform bordersTransform;

    private GameObject mainCamera;
    private Transform mainCameraTransform;

    private GameObject player;
    private Transform playerTransform;

    private Vector3 cameraFollowPosition = Vector3.zero;
    private Vector2 bordersFollowPosition = Vector2.zero;

    private Vector3 defaultCameraPosition = new Vector3(0, 0, -10);

    private const float smooth = 5f;

    private bool toFollow = false;


    private void Start()
    {
        FindObjects();
        InitializeVariables();
    }

    private void FindObjects()
    {
        borders = GameObject.FindGameObjectWithTag("Borders");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ResetFollow()
    {
        cameraFollowPosition = defaultCameraPosition;
        bordersFollowPosition = Vector2.zero;

        bordersTransform.position = Vector2.zero;
        mainCameraTransform.position = defaultCameraPosition;
    }

    private void InitializeVariables()
    {
        bordersTransform = borders.transform;
        mainCameraTransform = mainCamera.transform;
        playerTransform = player.transform;
        cameraFollowPosition = defaultCameraPosition;
    }

    private void FixedUpdate()
    {
        FollowXAxis();
    }

    public void StartFollow()
    {
        toFollow = true;
    }

    public void StopFollow()
    {
        toFollow = false;
    }

    private void FollowXAxis()
    {
        if (toFollow)
        {
            MoveBorders();
            MoveCamera();
        }
    }

    private void MoveBorders()
    {
        bordersFollowPosition.x = playerTransform.position.x;

        Vector2 a = bordersTransform.position;
        Vector2 b = bordersFollowPosition;
        bordersTransform.position = Vector3.Lerp(a, b, smooth * Time.deltaTime);

        //bordersTransform.position = b;
    }

    private void MoveCamera()
    {
        cameraFollowPosition.x = playerTransform.position.x;

        Vector3 a = mainCameraTransform.position;
        Vector3 b = cameraFollowPosition;
        mainCameraTransform.position = Vector3.Lerp(a, b, smooth * Time.deltaTime);

        //mainCameraTransform.position = b;
    }
}
