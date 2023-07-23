using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    public Camera cam;
    public GameObject tileHighlightPrefab;

    private MoveSelector move;

    private GameObject tileHighlight;

    void Awake()
    {
        move = GetComponent<MoveSelector>();
    }

    void Start ()
    {
        Vector2Int gridPoint = Geometry.GridPoint(0, 0);
        Vector3 point = Geometry.PointFromGrid(gridPoint);
        tileHighlight = Instantiate(tileHighlightPrefab, point, Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);

    }

    void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Vector3 point = hit.point;
            Vector2Int gridPoint = Geometry.GridFromPoint(point);

            if(!Geometry.isInsideGrid(gridPoint)) return;

            tileHighlight.SetActive(true);
            tileHighlight.transform.position = Geometry.PointFromGrid(gridPoint);

            if (Input.GetMouseButtonDown(0))
            {
                GameObject selectedPiece = GameManager.instance.PieceAtGrid(gridPoint);
                if (GameManager.instance.DoesPieceBelongToCurrentPlayer(selectedPiece))
                {
                    GameManager.instance.SelectPiece(selectedPiece);
                    
                    ExitState(selectedPiece);
                }
            }
        }
        else
        {
            tileHighlight.SetActive(false);
        }
    }

    public void EnterState()
    {
        enabled = true;
    }

    private void ExitState(GameObject movingPiece)
    {
        this.enabled = false;
        tileHighlight.SetActive(false);
        move.EnterState(movingPiece);
    }
}