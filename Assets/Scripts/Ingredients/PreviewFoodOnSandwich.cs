using Ingredient;
using UnityEngine;

public class PreviewFoodOnSandwich : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // Oyuncunun kamerasý
    [SerializeField] private LayerMask foodLayer; // Yiyecekler için layer

    private GameObject currentPreview;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }
    void Update()
    {
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100f, foodLayer))
        {
            ScriptableIngredientItem hoveredFood = hit.collider.GetComponent<IngredientItemBase>()?.IngredientItem;

            if (hoveredFood != null && hoveredFood.IsCompatibleWith(CurrentHeldFood))
            {
                ShowPreview(hit.point);
            }
            else
            {
                HidePreview();
            }
        }
        else
        {
            HidePreview();
        }

        if (Input.GetKeyDown(KeyCode.F) && currentPreview != null)
        {
            PlaceFood(currentPreview.transform.position);
        }
    }

    private void ShowPreview(Vector3 position)
    {
        if (currentPreview == null)
        {
            currentPreview = Instantiate(CurrentHeldFood.IngredientPrefab.gameObject);
            // Görsel fark için, preview objesine transparan bir malzeme eklenebilir.
        }

        currentPreview.transform.position = position;
    }

    private void HidePreview()
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }
    }

    private void PlaceFood(Vector3 position)
    {
        Instantiate(CurrentHeldFood.IngredientPrefab, position, Quaternion.identity);
        Destroy(currentPreview);
        currentPreview = null;
    }

    public ScriptableIngredientItem CurrentHeldFood { get; set; } // Oyuncunun elindeki yiyecek
}
