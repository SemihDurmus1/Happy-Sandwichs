using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject clickUI;

    public void ActivateUI(GameObject uiObject)
    {
        uiObject.SetActive(true);
        Debug.Log("UI activated: " + uiObject.name);
    }

    public void DeactivateUI(GameObject uiObject)
    {
        uiObject.SetActive(false);
        Debug.Log("UI deactivated: " + uiObject.name);
    }
}
