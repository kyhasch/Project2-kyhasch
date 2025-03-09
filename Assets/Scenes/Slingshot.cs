using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Slingshot : MonoBehaviour
{
    public GameObject launcher;
    public GameObject prefabBall;
    public GameObject activeBall;
    public Text scoreText;
    public float speedMultiplier = 10.0f;
    public LineRenderer lineRenderer;
    
    private bool isAiming;
    private float sphereRadius;
    public int shots = 0;

    // Audio components
    public AudioSource audioSource;  // Assign this in the inspector
    public AudioClip shootSound;     // Assign the shooting sound in the inspector

    void Start()
    {
        launcher.SetActive(false);
        isAiming = false;
        sphereRadius = GetComponent<SphereCollider>().radius;
        activeBall = null;
        scoreText.text = shots + "/5 remaining";

        // Ensure Line Renderer is set up
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }
    }

    void OnMouseEnter()
    {
        launcher.SetActive(true);
    }

    void OnMouseExit()
    {
        launcher.SetActive(false);
    }

    void OnMouseDown()
    {
        if (shots < 5)
        {
            isAiming = true;
            activeBall = Instantiate(prefabBall, launcher.transform.position, Quaternion.identity);
            activeBall.GetComponent<Rigidbody>().isKinematic = true;

            // Enable Line Renderer
            if (lineRenderer != null)
            {
                lineRenderer.enabled = true;
            }
        }
    }

    void Update()
    {
        if (!isAiming) return;

        Vector3 mousePositionScreen = Input.mousePosition;
        mousePositionScreen.z = -Camera.main.transform.position.z;
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        Vector3 dragVector = mousePositionWorld - launcher.transform.position;

        if (dragVector.magnitude > sphereRadius)
        {
            dragVector.Normalize();
            dragVector *= sphereRadius;
        }

        activeBall.transform.position = launcher.transform.position + dragVector;

        // Update the Line Renderer
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, launcher.transform.position);
            lineRenderer.SetPosition(1, activeBall.transform.position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isAiming = false;
            Rigidbody rb = activeBall.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.linearVelocity = -dragVector * speedMultiplier; // Ball is launched!

            // Play sound **after** setting velocity
            if (audioSource != null && shootSound != null)
            {
                Debug.Log("Playing shoot sound...");
                audioSource.PlayOneShot(shootSound);
            }
            else
            {
            Debug.LogWarning("AudioSource or shootSound is missing!");
            }

            // Disable Line Renderer after firing
            if (lineRenderer != null)
            {
                lineRenderer.enabled = false;
            }

            GameObject cam = GameObject.Find("Main Camera");
            cam.GetComponent<FollowTarget>().targetObject = activeBall;
            shots++;
            scoreText.text = "SHOTS Fired: " + shots;
        }
    }
}
