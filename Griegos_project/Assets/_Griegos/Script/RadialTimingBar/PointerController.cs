using UnityEngine;

public class PointerController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public RectTransform safeZone;
    public RectTransform midZone;
    public float moveSpeed = 100f;

    // Reference to the result/retry manager
    public RetryManager retryManager;

    // Prevents the player from getting multiple results
    private bool gameFinished = false;

    private RectTransform pointerTransform;
    private Vector3 targetPosition;

    void Awake()
    {
        pointerTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        StartQTE();
    }

    void Update()
    {
        // Stop the pointer after getting a result
        if (gameFinished)
            return;

        // Move the pointer towards the target position
        pointerTransform.position = Vector3.MoveTowards(pointerTransform.position,targetPosition,moveSpeed * Time.deltaTime);

        // Change direction when reaching the points
        if (Vector3.Distance(pointerTransform.position, pointA.position) < 0.1f)
        {
            targetPosition = pointB.position;
        }
        else if (Vector3.Distance(pointerTransform.position, pointB.position) < 0.1f)
        {
            targetPosition = pointA.position;
        }

        // Check for input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckResult();
        }
    }

    // Called when the pot has all the ingredients
    public void StartQTE()
    {
        gameFinished = false;

        // Put the pointer back at the beginning
        pointerTransform.position = pointA.position;

        // Make it move towards B
        targetPosition = pointB.position;
    }

    void CheckResult()
    {
        // Prevent multiple results
        gameFinished = true;

        // Check the green safe zone first
        if (RectTransformUtility.RectangleContainsScreenPoint(safeZone,pointerTransform.position,null))
        {
            Debug.Log("Success!");

            retryManager.ShowResult("Success");
        }

        // Check mediocre zone
        else if (RectTransformUtility.RectangleContainsScreenPoint(midZone,pointerTransform.position,null))
        {
            Debug.Log("Mediocre!");

            retryManager.ShowResult("Mediocre");
        }

        // Otherwise it's a fail
        else
        {
            Debug.Log("Fail!");

            retryManager.ShowResult("Fail");
        }
    }
}