using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    GameObject whoseTurnText;
    public bool isWhiteTurn;
    public Square[,] AllSquares = new Square[8,8];

    public GameObject UndoBtn;
    public PieceColorDisplayController SwitchPawnPanel;
    public bool AllowSquareSelection = false;
    public int[] CurrentSquareSelectionCoordinates = new int[2];
    public Square CurrentSquareSelection;
    public Piece CurrentPieceSelection;

    public List<Movement> allMoves = new List<Movement>();
    
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
 
                allMoves.Add(new Movement(turnNumber,
                                          CurrentPieceSelection.SquareOfPiece,
                                          CurrentPieceSelection,
                                          new NormalOrSpecialMove(CurrentSquareSelection),
                                          CurrentSquareSelection.PieceInSquare
                                          ));
                NormalPieceMovement();
        }
        else if (CurrentSquareSelection != null)
        {
            // TODO for special move
                if ((CurrentPieceSelection is WhitePawn) && (CurrentSquareSelection.indRow == 5))
                {
                        Debug.Log("White an ampersend");
                        allMoves.Add(new Movement(turnNumber,
                                              CurrentPieceSelection.SquareOfPiece,
                                              CurrentPieceSelection,
                                              new NormalOrSpecialMove(CurrentSquareSelection, true),
                                              AllSquares[CurrentSquareSelection.indRow -1 , CurrentSquareSelection.indCol].PieceInSquare
                                              ));


                        RemovePieceFrom(this.AllSquares[CurrentSquareSelection.indRow - 1, CurrentSquareSelection.indCol]);
                        MovePieceTo(CurrentPieceSelection, CurrentSquareSelection);
                        EndOfTurnStuff();
}
                else if ((CurrentPieceSelection is WhitePawn) && (CurrentSquareSelection.indRow == 7))
                {
                        Debug.Log("White piece promotion");
                        allMoves.Add(new Movement(turnNumber,
                                             CurrentPieceSelection.SquareOfPiece,
                                             CurrentPieceSelection,
                                             new NormalOrSpecialMove(CurrentSquareSelection, true),
                                             CurrentSquareSelection.PieceInSquare
                                             ));
                        NormalPieceMovement();
                        SwitchPawnPanel.WhitePiecesHandler(allMoves[allMoves.Count-1].MovedTo.theValidMove);
            }
                else if ((CurrentPieceSelection is BlackPawn) && (CurrentSquareSelection.indRow == 2))
                {
                        Debug.Log("Black an ampersend");
                        allMoves.Add(new Movement(turnNumber,
                                             CurrentPieceSelection.SquareOfPiece,
                                             CurrentPieceSelection,
                                             new NormalOrSpecialMove(CurrentSquareSelection, true),
                                             AllSquares[CurrentSquareSelection.indRow + 1, CurrentSquareSelection.indCol].PieceInSquare

                                             ));


                        RemovePieceFrom(this.AllSquares[CurrentSquareSelection.indRow + 1, CurrentSquareSelection.indCol]);
                        MovePieceTo(CurrentPieceSelection, CurrentSquareSelection);
                        EndOfTurnStuff();
                }
                else if ((CurrentPieceSelection is BlackPawn) && (CurrentSquareSelection.indRow == 0))
                {
                        Debug.Log("Black pawn promotion");
                        allMoves.Add(new Movement(turnNumber,
                                             CurrentPieceSelection.SquareOfPiece,
                                             CurrentPieceSelection,
                                             new NormalOrSpecialMove(CurrentSquareSelection, true),
                                             CurrentSquareSelection.PieceInSquare
                                             ));
                        NormalPieceMovement();
                        SwitchPawnPanel.BlackPiecesHandler(allMoves[allMoves.Count - 1].MovedTo.theValidMove);
            }
                else if ( CurrentPieceSelection is King)
                {
                        Debug.Log("king Castelling ");
                        allMoves.Add(new Movement(turnNumber,
                                             CurrentPieceSelection.SquareOfPiece,
                                             CurrentPieceSelection,
                                             new NormalOrSpecialMove(CurrentSquareSelection, true),
                                             CurrentSquareSelection.PieceInSquare
                                             ));
                        MovePieceTo(CurrentPieceSelection, CurrentSquareSelection);

                        if (CurrentSquareSelection.indCol == 2)
                        {
                            MovePieceTo(this.AllSquares[CurrentPieceSelection.SquareOfPiece.indRow, 0].PieceInSquare, this.AllSquares[CurrentPieceSelection.SquareOfPiece.indRow, 3]);
                        }
                        else
                        {
                            MovePieceTo(this.AllSquares[CurrentPieceSelection.SquareOfPiece.indRow, 7].PieceInSquare, this.AllSquares[CurrentPieceSelection.SquareOfPiece.indRow, 5]);
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
    private void MovePieceTo(Piece pieceToMove, Square placeToMoveTo)
    {
        pieceToMove.SquareOfPiece.PieceInSquare = null;
        pieceToMove.transform.SetParent(placeToMoveTo.gameObject.transform, false);
        placeToMoveTo.PieceInSquare = pieceToMove;
        placeToMoveTo.SetPieceCoordinates();
        //pieceToMove.SetSquareOfPiece();
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

        if (turnNumber != 1)
        {
            UndoBtn.SetActive(true);
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
        MovePieceTo(CurrentPieceSelection, CurrentSquareSelection);
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

    public void UndoMove()
    {

        Movement LastMove = allMoves[allMoves.Count -1];
        if (LastMove.MovedTo.isSpecial)
        {

            if (LastMove.PieceMoved is King)
            {
                if (LastMove.MovedTo.theValidMove.indCol == 2)
                {
                    MovePieceTo(this.AllSquares[LastMove.PieceMoved.SquareOfPiece.indRow, 3].PieceInSquare, this.AllSquares[LastMove.PieceMoved.SquareOfPiece.indRow, 0]);
                    this.AllSquares[LastMove.PieceMoved.SquareOfPiece.indRow, 0].PieceInSquare.movesMade -= 2;
                }
                else
                {
                    MovePieceTo(this.AllSquares[LastMove.PieceMoved.SquareOfPiece.indRow, 5].PieceInSquare, this.AllSquares[LastMove.PieceMoved.SquareOfPiece.indRow, 7]);
                    this.AllSquares[LastMove.PieceMoved.SquareOfPiece.indRow, 7].PieceInSquare.movesMade -= 2;
                }
            }
            else if (LastMove.PieceMoved is WhitePawn)
            {
                GameObject tempObject = LastMove.MovedTo.theValidMove.PieceInSquare.gameObject;
                int tempMovesMade = LastMove.MovedTo.theValidMove.PieceInSquare.movesMade;

                DestroyImmediate(LastMove.MovedTo.theValidMove.PieceInSquare);
                LastMove.MovedTo.theValidMove.PieceInSquare = null;

                WhitePawn ReAddedPiece = tempObject.AddComponent<WhitePawn>();
                LastMove.MovedTo.theValidMove.PieceInSquare.gameObject.GetComponentInChildren<RawImage>().texture = Resources.Load("Pawns/whitepawn") as Texture;

                ReAddedPiece.Initialize(true, tempMovesMade);

            }
            else if (LastMove.PieceMoved is BlackPawn)
            {
                GameObject tempObject = LastMove.MovedTo.theValidMove.PieceInSquare.gameObject;
                int tempMovesMade = LastMove.MovedTo.theValidMove.PieceInSquare.movesMade;

                DestroyImmediate(LastMove.MovedTo.theValidMove.PieceInSquare);
                LastMove.MovedTo.theValidMove.PieceInSquare = null;

                BlackPawn ReAddedPiece = tempObject.AddComponent<BlackPawn>();
                LastMove.MovedTo.theValidMove.PieceInSquare.gameObject.GetComponentInChildren<RawImage>().texture = Resources.Load("Pawns/blackpawn") as Texture;

                ReAddedPiece.Initialize(false, tempMovesMade);

            }

        }




        LastMove.MovedTo.theValidMove.PieceInSquare.transform.SetParent(LastMove.MovedFrom.gameObject.transform, false);
        LastMove.MovedFrom.PieceInSquare = LastMove.MovedTo.theValidMove.PieceInSquare;
        LastMove.MovedFrom.SetPieceCoordinates();
        LastMove.MovedTo.theValidMove.PieceInSquare.SetSquareOfPiece();
        LastMove.MovedTo.theValidMove.PieceInSquare.movesMade -= 1;
        LastMove.MovedTo.theValidMove.PieceInSquare = null;


        if (LastMove.CapturedPiece != null)
        {
            LastMove.CapturedPiece.transform.SetParent(LastMove.CapturedPiece.SquareOfPiece.transform, false);
            LastMove.CapturedPiece.GetComponent<Canvas>().enabled = true;
            LastMove.CapturedPiece.GetComponent<Canvas>().sortingOrder = 1;
            LastMove.CapturedPiece.SquareOfPiece.PieceInSquare = LastMove.CapturedPiece;
        }

        allMoves.RemoveAt(allMoves.Count - 1);
        --turnNumber;

        AllowSquareSelection = false;
        CurrentPieceSelection = null;
        CurrentSquareSelection = null;
        foreach (Square j in this.AllSquares) 
        {
            j.GetComponentInParent<Image>().color = new Color(j.GetComponentInParent<Image>().color.r,
                                                             j.GetComponentInParent<Image>().color.g,
                                                             j.GetComponentInParent<Image>().color.b,
                                                             1
                                                             );
        
        }




        if (turnNumber == 1)
        {
            UndoBtn.SetActive(false);
        }
        if (isWhiteTurn)
        {
            isWhiteTurn = false;
            whoseTurnText.GetComponent<Text>().text = "Black's turn";
        }
        else
        {
            isWhiteTurn = true;
            whoseTurnText.GetComponent<Text>().text = "White's turn";
        }



      //  StateOfGame = CheckSituationOfBoard();





    
    }









}
