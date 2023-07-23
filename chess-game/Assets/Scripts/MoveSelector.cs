using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
    public Camera cam;

    public GameObject moveLocationPrefab;
    public GameObject tileHighlightPrefab;
    public GameObject attackLocationPrefab;

    private GameObject tileHighlight;
    private GameObject movingPiece;
    private List<Vector2Int> moveLocations;
    private List<GameObject> locationHighlights;

    private LTDescr pieceTween;
    private Vector3 initialLocalPosition;

    TileSelector selector;

    void Awake()
    {
        selector = GetComponent<TileSelector>();
    }

    void Start ()
    {
        this.enabled = false;
        tileHighlight = Instantiate(tileHighlightPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)),
            Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);
    }

    void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
        if(Input.GetMouseButtonDown(1))
        {
            ResetPieceAnmation();
            CancelMove();
        }

        if (hit.collider != null)
        {
            Vector3 point = hit.point;
            Vector2Int gridPoint = Geometry.GridFromPoint(point);

            tileHighlight.SetActive(true);
            tileHighlight.transform.position = Geometry.PointFromGrid(gridPoint);
            if (Input.GetMouseButtonDown(0))
            {
                if (!moveLocations.Contains(gridPoint))
                {
                    return;
                }
                ResetPieceAnmation();
                if (GameManager.instance.PieceAtGrid(gridPoint) == null)
                {
                    GameManager.instance.Move(movingPiece, gridPoint);
                }
                else
                {
                    GameManager.instance.CapturePieceAt(gridPoint);
                    GameManager.instance.Move(movingPiece, gridPoint);
                }
                
                ExitState();
            }
        }
        else
        {
            tileHighlight.SetActive(false);
        }
    }

    private void ResetPieceAnmation()
    {
        movingPiece.transform.localPosition = initialLocalPosition;
    }

    private void CancelMove()
    {
        this.enabled = false;
        tileHighlight.SetActive(false);
        foreach (GameObject highlight in locationHighlights)
        {
            Destroy(highlight);
        }
        GameManager.instance.DeselectPiece(movingPiece);
        LeanTween.pause(movingPiece);
        movingPiece = null;
        selector.EnterState();
    }

    public void EnterState(GameObject piece)
    {
        movingPiece = piece;
        initialLocalPosition = movingPiece.transform.localPosition;
        this.enabled = true;

        moveLocations = GameManager.instance.MovesForPiece(movingPiece);
        locationHighlights = new List<GameObject>();

        if (moveLocations.Count == 0)
        {
            CancelMove();
        }

        foreach (Vector2Int loc in moveLocations)
        {
            GameObject highlight;
            if (GameManager.instance.PieceAtGrid(loc))
            {
                highlight = Instantiate(attackLocationPrefab, Geometry.PointFromGrid(loc), Quaternion.identity, gameObject.transform);
            }
            else
            {
                highlight = Instantiate(moveLocationPrefab, Geometry.PointFromGrid(loc), Quaternion.identity, gameObject.transform);
            }
            locationHighlights.Add(highlight);
        }

        pieceTween = LeanTween.moveY(movingPiece, movingPiece.transform.localPosition.y + 0.25f, 0.25f).setLoopPingPong();
    }

    private void ExitState()
    {
        CancelMove();
        GameManager.instance.NextPlayer();
    }
}
