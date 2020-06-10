using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceColorDisplayController : MonoBehaviour
{

    public GameObject SwitchPawnPanelCanvas;
    public GameObject WhitePiecesCanvas;
    public GameObject BlackPiecesCanvas;
    public List<PawnToPieceOnClick> AllPiecesSelection = new List<PawnToPieceOnClick>();

    void Awake()
    {
        foreach (PawnToPieceOnClick j in this.transform.GetComponentsInChildren<PawnToPieceOnClick>(true))
        {
            AllPiecesSelection.Add(j);
        }
    }

    public void BlackPiecesHandler(Square LocationOfPawn)
    {
        this.SwitchPawnPanelCanvas.SetActive(true);

        this.BlackPiecesCanvas.SetActive(true);
        foreach (PawnToPieceOnClick j in AllPiecesSelection)
        {
            j.SetLocationOfPawn(LocationOfPawn);
        }
    }


    public void WhitePiecesHandler(Square LocationOfPawn)
    {
        this.SwitchPawnPanelCanvas.SetActive(true);

        this.WhitePiecesCanvas.SetActive(true);
        foreach (PawnToPieceOnClick j in AllPiecesSelection)
        {
            j.SetLocationOfPawn(LocationOfPawn);
        }
    }



    public void ReturningToNormal()
    {
        this.SwitchPawnPanelCanvas.SetActive(false);
        this.WhitePiecesCanvas.SetActive(false);
        this.BlackPiecesCanvas.SetActive(false);
    }







}
