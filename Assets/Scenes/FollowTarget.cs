using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject targetObject;
    public Vector3 initialPosition;
    public GameObject ground;
    private Vector3 groundPosition;
    public float easing = 0.05f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = this.transform.position;
        groundPosition = ground.transform.position;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition;

        if (targetObject != null &&
            targetObject.GetComponent<Rigidbody>().linearVelocity.magnitude < 0.01f)
        {
            targetObject = null;
        }

        if (targetObject == null)
            targetPosition = initialPosition;
        else
            targetPosition = targetObject.transform.position; // Fixed missing assignment

        Vector3 followPosition = Vector3.Lerp(this.transform.position, targetPosition, easing); // Fixed typo

        if (followPosition.x < initialPosition.x)
        {
            followPosition.x = initialPosition.x;
        }
        if (followPosition.y < initialPosition.y)
        {
            followPosition.y = initialPosition.y;
        }
        followPosition.z = initialPosition.z;

        this.transform.position = followPosition;

        this.GetComponent<Camera>().orthographicSize = followPosition.y - groundPosition.y; // Fixed capitalization
    }
}
