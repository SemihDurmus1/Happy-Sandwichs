using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance;
    public Transform[] orderPoints;
    public Transform[] spawnPoints;
    public Transform[] leavePoints;

    private void Awake()//Singleton
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
