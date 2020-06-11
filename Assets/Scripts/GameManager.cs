using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    GameObject whoseTurnText;
    public bool isWhiteTurn;
    public Square[,] AllSquares = new Square[8,8];
    public PieceColorDisplayController SwitchPawnPanel;
    public bool AllowSquareSelection = false;
    public int[] CurrentSquareSelectionCoordinates = new int[2];
    public Square CurrentSquareSelection;
    public Piece CurrentPieceSelection;
    public Piece LastPieceMoved = null;
    public int turnNumber = 1;



    // Start is called before the first frame update
    void Start()
    {
        isWhiteTurn = true;
        Square[] AllSquaresOneDim = GameObject.FindObjectsOfType<Square>();
        foreach (Square t in AllSquaresOneDim)
        {
            AllSquares[t.indRow, t.indCol] = t;
        }
        whoseTurnText = GameObject.Find("TurnText");
    }
    // custom method
    public void CheckSelection() 
    {
         AllowSquareSelection = true;
        // for non special move
        if (CurrentSquareSelection != null && !isSquareSpecialMove(CurrentPieceSelection.validMoves, CurrentSquareSelection))
        {
                LastPieceMoved = CurrentPieceSelection;
                NormalPieceMovement();
        }
        else if (CurrentSquareSelection != null)
        {
            // TODO for special move
                if ((CurrentPieceSelection is WhitePawn) && (CurrentSquareSelection.indRow == 5))
                {
                        Debug.Log("White an ampersend");
                        LastPieceMoved = CurrentPieceSelection;
                        RemovePieceFrom(this.AllSquares[CurrentSquareSelection.indRow - 1, CurrentSquareSelection.indCol]);
                        movePieceTo(CurrentPieceSelection, CurrentSquareSelection);
                        EndOfTurnStuff();
                }
                else if ((CurrentPieceSelection is WhitePawn) && (CurrentSquareSelection.indRow == 7))
                {
                        Debug.Log("White piece promotion");
                        LastPieceMoved = CurrentPieceSelection;
                        NormalPieceMovement();
                        SwitchPawnPanel.WhitePiecesHandler(LastPieceMoved.SquareOfPiece);
            }
                else if ((CurrentPieceSelection is BlackPawn) && (CurrentSquareSelection.indRow == 2))
                {
                        Debug.Log("Black an ampersend");
                        LastPieceMoved = CurrentPieceSelection;
                        RemovePieceFrom(this.AllSquares[CurrentSquareSelection.indRow + 1, CurrentSquareSelection.indCol]);
                        movePieceTo(CurrentPieceSelection, CurrentSquareSelection);
                        EndOfTurnStuff();
                }
                else if ((CurrentPieceSelection is BlackPawn) && (CurrentSquareSelection.indRow == 0))
                {
                        Debug.Log("Black pawn promotion");
                        LastPieceMoved = CurrentPieceSelection;
                        NormalPieceMovement();
                        SwitchPawnPanel.BlackPiecesHandler(LastPieceMoved.SquareOfPiece);
            }
                else if ( CurrentPieceSelection is King)
                {
                        Debug.Log("king Castelling ");
                        LastPieceMoved = CurrentPieceSelection;
                        movePieceTo(CurrentPieceSelection, CurrentSquareSelection);

                        if (CurrentSquareSelection.indCol == 2)
                        {
                            movePieceTo(this.AllSquares[CurrentPieceSelection.SquareOfPiece.indRow, 0].PieceInSquare, this.AllSquares[CurrentPieceSelection.SquareOfPiece.indRow, 3]);
                        }
                        else
                        {
                            movePieceTo(this.AllSquares[CurrentPieceSelection.SquareOfPiece.indRow, 7].PieceInSquare, this.AllSquares[CurrentPieceSelection.SquareOfPiece.indRow, 5]);
                        }






                EndOfTurnStuff();
                }
        }
    }













    private bool isSquareSpecialMove(List<NormalOrSpecialMove> theList, Square theElemnt)
    {
        foreach (NormalOrSpecialMove n in theList)
        {
            if (n.theValidMove == theElemnt && n.isSpecial)
            {
                return true;
            }
        }
        return false;
    
    }
    private void movePieceTo(Piece pieceToMove, Square placeToMoveTo)
    {
        pieceToMove.SquareOfPiece.PieceInSquare = null;
        pieceToMove.transform.SetParent(placeToMoveTo.gameObject.transform, false);
        placeToMoveTo.PieceInSquare = pieceToMove;
        placeToMoveTo.SetPieceCoordinates();
        pieceToMove.SetSquareOfPiece();
        pieceToMove.movesMade += 1;
        turnNumber += 1;
    }
    private void RemovePieceFrom(Square theParentSquare)
    {
        foreach (Transform child in theParentSquare.transform)
        {
            child.SetParent(null,false);
            child.GetComponent<Canvas>().enabled = false;
            child.GetComponent<Canvas>().sortingOrder = -1;
        }
        theParentSquare.PieceInSquare = null;



    }
    public void GameBtnHandler() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    private void EndOfTurnStuff()
    {
        AllowSquareSelection = false;
        CurrentPieceSelection = null;
        CurrentSquareSelection = null;

        foreach (Square j in this.AllSquares)
        {
            j.GetComponentInParent<Image>().color = new Color(
                                                              j.GetComponentInParent<Image>().color.r,
                                                              j.GetComponentInParent<Image>().color.g,
                                                              j.GetComponentInParent<Image>().color.b, 1);
        }

        if (isWhiteTurn)
        {
            isWhiteTurn = false;
            whoseTurnText.GetComponent<Text>().text = "Blacks turn";
        }
        else
        {
            isWhiteTurn = true;
            whoseTurnText.GetComponent<Text>().text = "Whites turn";
        }
    }
    private void NormalPieceMovement()
    {
        RemovePieceFrom(CurrentSquareSelection);
        movePieceTo(CurrentPieceSelection, CurrentSquareSelection);
        EndOfTurnStuff();
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











}
