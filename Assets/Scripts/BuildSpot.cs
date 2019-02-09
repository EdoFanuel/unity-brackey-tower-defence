using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpot : MonoBehaviour
{
    [Header("Highlight")]
    public Color AvailableColor;
    public Color TakenColor;
    private Color defaultColor;
    private Renderer render;

    [Header("Tower")]
    public Vector3 TowerOffset;
    private GameObject currentTurret;

    private void Start()
    {
        render = GetComponent<Renderer>();
        defaultColor = render.material.color;
    }

    private void OnMouseEnter()
    {
        render.material.color = currentTurret == null ? AvailableColor : TakenColor;
    }

    private void OnMouseExit()
    {
        render.material.color = defaultColor;
    }

    private void OnMouseDown()
    {
        if (currentTurret != null)
        {
            //TODO: display on screen, or... do something: sell it, upgrade it, caress it gently, etc
            Debug.Log("Cannot build there");
            return;
        }

        GameObject newTurret = TowerManager.Instance.SelectedTower;
        currentTurret = Instantiate(newTurret, transform.position + TowerOffset, transform.rotation);
    }
}
