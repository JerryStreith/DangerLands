using UnityEngine;
using System.Collections;

public class newcam : MonoBehaviour
{
    public GameObject targetObject;
    private float distanceToTarget;

    public Vector3 endPos;
    Vector3 startPos;
    void Start()
    {
       startPos = transform.position;
        distanceToTarget = transform.position.x - targetObject.transform.position.x;
    }

    void Update()
    {
        if (targetObject != null)
        {
            float targetObjectX = targetObject.transform.position.x;
            Vector3 newCameraPosition = transform.position;
            newCameraPosition.x = Mathf.Clamp(targetObjectX + distanceToTarget, startPos.x, endPos.x);
            transform.position = newCameraPosition;
        }
    }
}