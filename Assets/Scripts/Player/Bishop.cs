using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Bishop : Piece
{
    public override void CheckValidMoves()
    {
        Square checkUpLeftSquare = null;
        Square checkUpRightSquare = null;
        Square checkDownRightSquare = null;
        Square checkDownLeftSquare = null;


        validMoves.Clear();
        foreach (Square j in TheCanvas.AllSquares)
        {
            if (
                (
                    ((j.indRow - this.SquareOfPiece.indRow) == (j.indCol - this.SquareOfPiece.indCol)) ||
                    ((j.indRow - this.SquareOfPiece.indRow) == -(j.indCol - this.SquareOfPiece.indCol))
                )
               )
            {
                validMoves.Add(new NormalOrSpecialMove(j));

                if (j.gameObject.transform.childCount != 0)
                {
                    if (j.indRow < this.SquareOfPiece.indRow && j.indCol > this.SquareOfPiece.indCol)
                    {
                        checkDownRightSquare = j;
                    }
                    else if (j.indRow < this.SquareOfPiece.indRow && j.indCol < this.SquareOfPiece.indCol)
                    {
                        checkDownLeftSquare = j;
                    }
                    else if (j.indRow > this.SquareOfPiece.indRow && j.indCol < this.SquareOfPiece.indCol)
                    {
                        if (checkUpLeftSquare == null)
                        {
                            checkUpLeftSquare = j;
                        }
                    }
                    else if (j.indRow > this.SquareOfPiece.indRow && j.indCol > this.SquareOfPiece.indCol)
                    {
                        if (checkUpRightSquare == null)
                        {
                            checkUpRightSquare = j;
                        }
                    }
                }
            }
        }










        // copy for  downleft, downright square
        List<NormalOrSpecialMove> validMovesCopy = new List<NormalOrSpecialMove>();
        foreach (NormalOrSpecialMove j in validMoves)
        {
            validMovesCopy.Add(j);
        }




       // working on upleft , upright square on the way   
        foreach (NormalOrSpecialMove j in validMovesCopy) 
        {
            if (
                    (  j.theValidMove.gameObject.transform.childCount != 0 && j.theValidMove.PieceInSquare.isWhite == this.isWhite                                        )  ||
                    (  checkUpLeftSquare    != null && (j.theValidMove.indRow > checkUpLeftSquare.indRow    && j.theValidMove.indCol < checkUpLeftSquare.indCol     )     )  ||
                    (  checkUpRightSquare   != null && (j.theValidMove.indRow > checkUpRightSquare.indRow   && j.theValidMove.indCol > checkUpRightSquare.indCol    )     ) 
               )
            {
                validMoves.Remove(j);
            }
        }






        // working on downLeft, DownRight square on the way
        validMovesCopy.Reverse();

        bool foundDownLeft = false;
        bool foundDownRight = false;

        foreach (NormalOrSpecialMove j in validMovesCopy)
        {
            if (checkDownLeftSquare != null && j.theValidMove == checkDownLeftSquare)
            {
                foundDownLeft = true;
            }
            else if (checkDownRightSquare != null && j.theValidMove == checkDownRightSquare)
            {
                foundDownRight = true;
            }


            if (
                  (foundDownLeft && (j.theValidMove.indRow < checkDownLeftSquare.indRow  && j.theValidMove.indCol < checkDownLeftSquare.indCol  )) ||
                  (foundDownRight && (j.theValidMove.indRow < checkDownRightSquare.indRow && j.theValidMove.indCol > checkDownRightSquare.indCol ))
                ) 
            {
                validMoves.Remove(j);
            }
            
         
        }












    }


}
