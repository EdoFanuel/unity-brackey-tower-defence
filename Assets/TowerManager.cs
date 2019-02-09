using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;

    public GameObject SelectedTower { get; private set; }
    public List<GameObject> AvailableTowers;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one TowerManager detected!");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SelectedTower = AvailableTowers[0];
    }
}
