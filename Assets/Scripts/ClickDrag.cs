using UnityEngine;

public class ClickDrag : MonoBehaviour
{
    public float forceAmount = 500;

    private Rigidbody dragObject;
    private Camera camera;

    private float selectionDistance;

    void Start()
    {
        //FindFirstObjectByType<Camera>();

        camera = Camera.main ? Camera.main : GetComponent<Camera>();
    }

    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                selectionDistance = hit.distance;

                dragObject = hit.rigidbody;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragObject = null;
        }

        if (dragObject != null)
        {
            selectionDistance += Input.mouseScrollDelta.y;
        }
    }

    private void FixedUpdate()
    {
        if (dragObject)
        {
            Vector3 cameraObjectDelta = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));

            Vector3 objectMoveDirection = cameraObjectDelta - dragObject.transform.position;

            dragObject.linearVelocity = objectMoveDirection * forceAmount;
        }
    }
}
