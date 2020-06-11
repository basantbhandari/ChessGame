using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Rook : Piece
{
    public override void CheckValidMoves()
    {



        Square checkUpSquare = null;
        Square checkRightSquare = null;
        Square checkDownSquare = null;
        Square checkLeftSquare = null;

        validMoves.Clear();
        kingCantMoveHere.Clear();

        foreach (Square j in TheCanvas.AllSquares)
        {
       



            if (((this.SquareOfPiece.indRow == j.indRow) || (this.SquareOfPiece.indCol == j.indCol)))
            {
                validMoves.Add(new NormalOrSpecialMove(j));

                if (j.gameObject.transform.childCount != 0)
                {
                    if (j.indRow < this.SquareOfPiece.indRow)
                    {
                        checkDownSquare = j;
                    }
                    else if (j.indCol < this.SquareOfPiece.indCol)
                    {
                        checkLeftSquare = j;
                    }
                    else if (j.indCol > this.SquareOfPiece.indCol)
                    {
                        if (checkRightSquare == null)
                        {
                            checkRightSquare = j;
                        }
                    }
                    else if (j.indRow > this.SquareOfPiece.indRow)
                    {
                        if (checkUpSquare == null)
                        {
                            checkUpSquare = j;
                        }
                    }

                }

            }












        }





        //copy of valid move for up ,right square
        List<NormalOrSpecialMove> validMoveCopy = new List<NormalOrSpecialMove>();

        foreach (NormalOrSpecialMove j in validMoves)
        {
            validMoveCopy.Add(j);
        }


        foreach (NormalOrSpecialMove j in validMoves)
        {
            kingCantMoveHere.Add(j);
        }




        bool foundPieceRight = false;
        bool foundPieceUp = false;


        // working on up , right square on the way
        foreach (NormalOrSpecialMove j in validMoveCopy)
        {
            if (
                    (j.theValidMove.gameObject.transform.childCount != 0 &&    j.theValidMove.PieceInSquare.isWhite == this.isWhite)    ||
                    (checkRightSquare != null                            &&    j.theValidMove.indCol > checkRightSquare.indCol     )    ||
                    (checkUpSquare    != null                            &&    j.theValidMove.indRow > checkUpSquare.indRow        )
                )
            {
                validMoves.Remove(j);


                if (
                    (checkRightSquare != null && !(checkRightSquare.PieceInSquare is King) || (foundPieceRight)) && 
                    (checkRightSquare != null) &&
                    (j.theValidMove.indRow == checkRightSquare.indRow)
                    )
                {
                    kingCantMoveHere.Remove(j);
                    if (foundPieceRight == false) 
                    {
                        foundPieceRight = true;
                    }
                
                }
                else if (
                  (checkUpSquare != null && !(checkUpSquare.PieceInSquare is King) || (foundPieceUp)) &&
                  (checkUpSquare != null) &&
                  (j.theValidMove.indCol == checkUpSquare.indCol)
                  )
                {
                    kingCantMoveHere.Remove(j);
                    if (foundPieceUp == false)
                    {
                        foundPieceUp = true;
                    }

                }




            }
        }





        // working on down, left square on the way
        validMoveCopy.Reverse();

        bool foundLeft = false;
        bool foundDown = false;

        bool foundPieceLeft = false;
        bool foundPieceDown = false;

        foreach (NormalOrSpecialMove j in validMoveCopy)
        {
            if (checkLeftSquare != null && j.theValidMove == checkLeftSquare)
            {
                foundLeft = true;
            }
            else if (checkDownSquare != null && j.theValidMove == checkDownSquare)
            {
                foundDown = true;
            }

            if (
                    (foundLeft && (j.theValidMove.indCol < checkLeftSquare.indCol)) ||
                    (foundDown && (j.theValidMove.indRow < checkDownSquare.indRow))
                )
            {
                validMoves.Remove(j);
            }









            if (
                (checkLeftSquare != null)  &&
                (j.theValidMove == checkLeftSquare) &&
                (!(checkLeftSquare.PieceInSquare is King))
               ) 
            {
                foundPieceLeft = true;
            }
            else if (
               (checkDownSquare != null) &&
               (j.theValidMove == checkDownSquare) &&
               (!(checkDownSquare.PieceInSquare is King))
              )
            {
                foundPieceDown = true;
            }

            if (
                    (foundPieceLeft && (j.theValidMove.indCol < checkLeftSquare.indCol)) ||
                    (foundPieceDown && (j.theValidMove.indRow < checkDownSquare.indRow))
                )
            {
                kingCantMoveHere.Remove(j);
            }










        }

















    }
}




