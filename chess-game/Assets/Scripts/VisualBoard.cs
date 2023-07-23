using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBoard : MonoBehaviour
{
    public GameObject AddPiece(GameObject piece, int row, int col)
    {
        Debug.Log(piece.name);
        Vector2Int gridPoint = Geometry.GridPoint(row, col);
        Vector3 position = Geometry.PointFromGrid(gridPoint);
        GameObject newPiece = Instantiate(piece, position, Quaternion.identity, gameObject.transform);
        Debug.Log(gridPoint);
        Debug.Log(position);
        return newPiece;
    }

    public void RemovePiece(GameObject piece)
    {
        Destroy(piece);
    }

    public void MovePiece(GameObject piece, Vector2Int gridPoint)
    {
        piece.transform.position = Geometry.PointFromGrid(gridPoint);
    }

    public void SelectPiece(GameObject piece)
    {
        // MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        // renderers.material = selectedMaterial;
    }

    public void DeselectPiece(GameObject piece)
    {
        // MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        // renderers.material = defaultMaterial;
    }
}
