
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;

    private Vector2 cursorHotspot;

    void Start()
    {
        if (cursorTexture != null)
        {
            cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
            Cursor.SetCursor(cursorTexture,cursorHotspot,CursorMode.Auto);
        }
    }

    void Update()
    {
        bool isLooking = Input.GetMouseButton(1);

        if (isLooking)
        {
            // Hide the cursor and lock it to the screen centre.
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            // Show the custom cursor and allow interaction.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
