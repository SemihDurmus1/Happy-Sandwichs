using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject clickUI;

    public void ActivateUI(GameObject uiObject)
    {
        uiObject.SetActive(true);
    }

    public void DeactivateUI(GameObject uiObject)
    {
        uiObject.SetActive(false);
    }
}
