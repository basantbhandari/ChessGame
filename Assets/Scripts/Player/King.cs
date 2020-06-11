using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class King : Piece
{
    public override void CheckValidMoves()
    {
        validMoves.Clear();
        kingCantMoveHere.Clear();

        foreach (Square j in TheCanvas.AllSquares)
        {
            // condition for king valid movement
            if (
                   (
                     (((j.indCol - this.SquareOfPiece.indCol) <= 1) && ((j.indCol - this.SquareOfPiece.indCol) >= -1)) &&
                     (((j.indRow - this.SquareOfPiece.indRow) <= 1) && ((j.indRow - this.SquareOfPiece.indRow) >= -1))
                   )
               )
            {
                kingCantMoveHere.Add(new NormalOrSpecialMove(j));
                if (
                           ((j.gameObject.transform.childCount == 0) || (this.isWhite != j.PieceInSquare.isWhite)) &&
                           ((this.isWhite && !DoesListContainElement(TheCanvas.blackMoves, j)) ||
                           (!this.isWhite && !DoesListContainElement(TheCanvas.blackMoves, j))  )
                       )
                    {
                        validMoves.Add(new NormalOrSpecialMove(j));
                    }
            }
            // this condition for castling => to make king safe from enemy by exchanging  rook and king
            //(type => 1) kingSide and 2) QueenSide)
            // queenside:   r _ _ _ k => _ _ k r _
            // kingside:    k _ _ r => _ r k _

            else if (
                        // property common in both type castling
                        (this.movesMade == 0)        &&
                        (j.indRow == this.SquareOfPiece.indRow) &&
                        (
                                // for queenside castling:   r _ _ _ k => _ _ k r _
                                (
                                    (TheCanvas.AllSquares[this.SquareOfPiece.indRow, 0].PieceInSquare != null) &&
                                    (TheCanvas.AllSquares[this.SquareOfPiece.indRow, 0].PieceInSquare.movesMade == 0) &&
                                    (j.indCol == 2) &&
                                    (TheCanvas.AllSquares[this.SquareOfPiece.indRow, 1].PieceInSquare == null) &&
                                    (TheCanvas.AllSquares[this.SquareOfPiece.indRow, 2].PieceInSquare == null) &&
                                    (TheCanvas.AllSquares[this.SquareOfPiece.indRow, 3].PieceInSquare == null)
                                )
                                ||

                                // for kingside castling:    k _ _ r => _ r k _
                                (
                                    (TheCanvas.AllSquares[this.SquareOfPiece.indRow, 7].PieceInSquare != null) &&
                                    (TheCanvas.AllSquares[this.SquareOfPiece.indRow, 7].PieceInSquare.movesMade == 0) &&
                                    (j.indCol == 6) &&
                                    (TheCanvas.AllSquares[this.SquareOfPiece.indRow, 5].PieceInSquare == null) &&
                                    (TheCanvas.AllSquares[this.SquareOfPiece.indRow, 6].PieceInSquare == null)
                                 )
                        )


                   )
                {
                  validMoves.Add(new NormalOrSpecialMove(j, true, false));
                }
        }




















    }
}



