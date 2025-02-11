using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private bool isCursorLocked = true; //Locked on start

    void Start()
    {
        ToggleCursor(false); // Lock
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Show the cursor when pressed ESC
        {
            ToggleCursor(!isCursorLocked);
        }
    }

    public void ToggleCursor(bool showCursor)
    {
        isCursorLocked = !showCursor;
        Cursor.visible = showCursor;
        Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked; // Lock or unlock the cursor
    }
}
