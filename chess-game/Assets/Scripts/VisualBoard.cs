using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBoard : MonoBehaviour
{
    private Vector3 initialLocalPosition;

    public GameObject AddPiece(GameObject piece, int row, int col)
    {
        Vector2Int gridPoint = Geometry.GridPoint(row, col);
        Vector3 position = Geometry.PointFromGrid(gridPoint);
        GameObject newPiece = Instantiate(piece, position, Quaternion.identity, gameObject.transform);
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
        //LeanTween.moveY(movingPiece, movingPiece.transform.localPosition.y + 0.25f, 0.25f).setLoopPingPong();
    }

    public void DeselectPiece(GameObject piece)
    {
        //LeanTween.pause(movingPiece);
        //movingPiece.transform.localPosition = initialLocalPosition;
    }
}
