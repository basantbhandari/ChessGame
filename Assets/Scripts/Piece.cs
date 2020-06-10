using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Piece : MonoBehaviour, IPointerDownHandler
{
    public bool isWhite;
    public int indRow;
    public int indCol;
    public int movesMade;

    public List<NormalOrSpecialMove> validMoves = new List<NormalOrSpecialMove>();
    public GameManager TheCanvas;
    public Square SquareOfPiece;



    void Awake()
    {
        SetSquareOfPiece();
        movesMade = 0;
        TheCanvas = GameObject.FindObjectOfType<GameManager>();
        SquareOfPiece.SetPieceCoordinates();

      
    }

    public void Initialize(bool inpIsWhite, int inpMovesMade = 1)
    {
        this.isWhite = inpIsWhite;
        this.movesMade = inpMovesMade;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.isWhite == TheCanvas.isWhiteTurn)
        {
            SelectPiece();
        }
        else if (TheCanvas.AllowSquareSelection)
        {
            this.transform.parent.gameObject.GetComponent<Square>().SelectSquare();
        }
    }
    public void SelectPiece()
    {
        if (this.isWhite == TheCanvas.isWhiteTurn)
        {

            foreach (Square j in TheCanvas.AllSquares)
            {
                j.GetComponentInParent<Image>().color = new Color( 
                                                                  j.GetComponentInParent<Image>().color.r,
                                                                  j.GetComponentInParent<Image>().color.g, 
                                                                  j.GetComponentInParent<Image>().color.b, 1);
            }
            CheckValidMoves();

            TheCanvas.CurrentPieceSelection = this;
            TheCanvas.CheckSelection();
        }
    }
    public void SetSquareOfPiece()
    {
        SquareOfPiece = this.GetComponentInParent<Square>();
    }
    public virtual void CheckValidMoves()
    {




        validMoves.Clear();
        foreach (Square j in TheCanvas.AllSquares)
        {
            if (j.gameObject.transform.childCount == 0)
            {
                validMoves.Add(new NormalOrSpecialMove(j));
            }
            else if (this.isWhite != j.PieceInSquare.isWhite)
            {
                validMoves.Add(new NormalOrSpecialMove(j));
            }
        }





        // used to blour the unvalid move
        foreach (Square j in TheCanvas.AllSquares)
        {
            if (this.validMoves.Count != 0)
            {
                foreach (NormalOrSpecialMove n in validMoves)
                {
                    if (j == n.theValidMove)
                    {
                        j.GetComponentInParent<Image>().color = new Color(j.GetComponentInParent<Image>().color.r,
                                                                        j.GetComponentInParent<Image>().color.g,
                                                                        j.GetComponentInParent<Image>().color.b,
                                                                        1);
                        break;
                    }
                    else
                    {
                        j.GetComponentInParent<Image>().color = new Color(j.GetComponentInParent<Image>().color.r,
                                                                        j.GetComponentInParent<Image>().color.g,
                                                                        j.GetComponentInParent<Image>().color.b,
                                                                        (float)0.45);
                    }
                }
            }
            else
            {
                j.GetComponentInParent<Image>().color = new Color(j.GetComponentInParent<Image>().color.r,
                                                                  j.GetComponentInParent<Image>().color.g,
                                                                  j.GetComponentInParent<Image>().color.b,
                                                                  (float)0.45);
            }
        }












    }



   
    










}
