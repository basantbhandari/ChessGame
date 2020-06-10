using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Square : MonoBehaviour, IPointerDownHandler
{


    public int indCol;
    public int indRow;
    public Piece PieceInSquare = null;
    public GameManager TheCanvas;


    private void Start()
    {
        TheCanvas = GameObject.FindObjectOfType<GameManager>();
        if (this.gameObject.transform.childCount != 0)
        {
            //PieceInSquare = GetComponentInChildren<Piece>();
            SetPieceCoordinates();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        SelectSquare();
    }
    public void SetPieceCoordinates()
    {
        PieceInSquare = GetComponentInChildren<Piece>();
        PieceInSquare.indRow = indRow;
        PieceInSquare.indCol = indCol;
        PieceInSquare.SquareOfPiece = this;
    }
    private bool DoesListContainElement(List<NormalOrSpecialMove> theList, Square theElement)
    {
        foreach (NormalOrSpecialMove n in theList)
        {
            if (n.theValidMove == theElement) 
            {
                return true;
            }
        }
        return false;
    }
    public void SelectSquare()
    {
        if ( 
            TheCanvas.AllowSquareSelection                                                        && 
            this.gameObject.transform.childCount == 0                                             && 
            DoesListContainElement(TheCanvas.CurrentPieceSelection.validMoves,this)
           )
        {
            TheCanvas.CurrentSquareSelection = this;
            TheCanvas.CheckSelection();
        }
        else if (
                this.gameObject.transform.childCount != 0                                         &&
                PieceInSquare.isWhite == TheCanvas.isWhiteTurn
             )
        {
            PieceInSquare.SelectPiece();
        }
        else if (
                this.gameObject.transform.childCount != 0                                         && 
                PieceInSquare.isWhite != TheCanvas.isWhiteTurn                                    && 
                DoesListContainElement(TheCanvas.CurrentPieceSelection.validMoves, this)
             )
        {
            TheCanvas.CurrentSquareSelection = this;
            TheCanvas.CheckSelection();
        }
        else
        {
            TheCanvas.CurrentPieceSelection = null;
            TheCanvas.AllowSquareSelection = false;
        }
    }


















}
