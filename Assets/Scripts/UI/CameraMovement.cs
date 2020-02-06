using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance = null;

    public Transform cameraTarget;
    public float cameraSmoothing;
    float currentCameraSize;

    public float cameraWidth;
    public float cameraHeight;

    public float cameraZoomRate = 7f;
    public float targetCameraZoom;

    public bool isIndoor = false;


    public Vector2 maxPosition;
    public Vector2 minPosition;

    public Text mapSizeText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        cameraWidth = 14.3f;
        cameraHeight = 8f;
        minPosition.x = cameraWidth;
        minPosition.y = cameraHeight;
        
        targetCameraZoom = Camera.main.orthographicSize;

    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Follow the player
        if (transform.position != cameraTarget.position)
        {
            Vector3 targetPosition = new Vector3(cameraTarget.position.x, cameraTarget.position.y, transform.position.z);

            if (!isIndoor)
            {
                targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            }

            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSmoothing);
        }

        //Camera Zoom
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            targetCameraZoom += Input.GetAxis("Mouse ScrollWheel") * cameraZoomRate * -1;
            currentCameraSize += Input.GetAxis("Mouse ScrollWheel") * cameraZoomRate * -1;
        }
        currentCameraSize = Camera.main.orthographicSize;

        Camera.main.orthographicSize = Mathf.Lerp(currentCameraSize, targetCameraZoom, cameraSmoothing);
    }

    public void SetCameraMax(int x, int y)
    {
        maxPosition.x = x - cameraWidth;
        maxPosition.y = y - cameraHeight;
    }
}
