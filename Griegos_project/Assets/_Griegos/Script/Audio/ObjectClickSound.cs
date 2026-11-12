
using UnityEngine;

public class ObjectClickSound : MonoBehaviour
{
    public Camera playerCamera;
    public AudioSource clickAudio;
    public AudioClip clickSound;

    public float interactionDistance = 5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(
                Input.mousePosition
            );

            if (Physics.Raycast(ray, out RaycastHit hit,
                interactionDistance))
            {
                clickAudio.PlayOneShot(clickSound);
            }
        }
    }
}
