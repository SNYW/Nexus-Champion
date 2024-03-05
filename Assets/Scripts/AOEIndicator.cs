using UnityEngine;

public class AOEIndicator : MonoBehaviour
{
    public LayerMask rayLayer;
    public Transform spellPositionIndicator;
    public Vector3 indicatorOffset;

    private void Update()
    {
        if (MouseManager.GetMousePositionOnNavmesh(rayLayer, transform.position, out var pos))
        {
            spellPositionIndicator.transform.position = pos + indicatorOffset;
        }
    }
}
