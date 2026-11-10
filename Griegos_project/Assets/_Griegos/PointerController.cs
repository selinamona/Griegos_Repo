
using UnityEngine;

public class PointerController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public RectTransform safeZone;
    public RectTransform midZone;
    public float moveSpeed = 100f;

    public RetryManager retryManager;

    // NEW: Reference to the CookingPot
    public CookingPot cookingPot;

    private bool gameFinished = false;

    private RectTransform pointerTransform;
    private Vector3 targetPosition;

    void Start()
    {
        pointerTransform = GetComponent<RectTransform>();
        targetPosition = pointB.position;
    }

    void Update()
    {
        if (gameFinished)
            return;

        pointerTransform.position = Vector3.MoveTowards(
            pointerTransform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(pointerTransform.position, pointA.position) < 0.1f)
        {
            targetPosition = pointB.position;
        }
        else if (Vector3.Distance(pointerTransform.position, pointB.position) < 0.1f)
        {
            targetPosition = pointA.position;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckResult();
        }
    }

    void CheckResult()
    {
        gameFinished = true;

        if (RectTransformUtility.RectangleContainsScreenPoint(
            safeZone, pointerTransform.position, null))
        {
            Debug.Log("Success!");

            // NEW: Finish cooking instead of showing Retry
            cookingPot.FinishCooking();
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(
            midZone, pointerTransform.position, null))
        {
            Debug.Log("Mediocre!");
            retryManager.ShowResult("Mediocre");
        }
        else
        {
            Debug.Log("Fail!");
            retryManager.ShowResult("Fail");
        }
    }
}
