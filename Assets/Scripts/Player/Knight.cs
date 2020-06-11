using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Knight : Piece
{
    public override void CheckValidMoves()
    {
        validMoves.Clear();
        kingCantMoveHere.Clear();
        foreach (Square j in TheCanvas.AllSquares)
        {
            if (
                    (

                        ((j.indRow == (this.SquareOfPiece.indRow + 1)) && ((j.indCol == (this.SquareOfPiece.indCol - 2)))) ||
                        ((j.indRow == (this.SquareOfPiece.indRow + 2)) && ((j.indCol == (this.SquareOfPiece.indCol - 1)))) ||
                        ((j.indRow == (this.SquareOfPiece.indRow + 1)) && ((j.indCol == (this.SquareOfPiece.indCol + 2)))) ||
                        ((j.indRow == (this.SquareOfPiece.indRow + 2)) && ((j.indCol == (this.SquareOfPiece.indCol + 1)))) ||
                        ((j.indRow == (this.SquareOfPiece.indRow - 1)) && ((j.indCol == (this.SquareOfPiece.indCol + 2)))) ||
                        ((j.indRow == (this.SquareOfPiece.indRow - 2)) && ((j.indCol == (this.SquareOfPiece.indCol + 1)))) ||
                        ((j.indRow == (this.SquareOfPiece.indRow - 1)) && ((j.indCol == (this.SquareOfPiece.indCol - 2)))) ||
                        ((j.indRow == (this.SquareOfPiece.indRow - 2)) && ((j.indCol == (this.SquareOfPiece.indCol - 1))))


                    )
             )
            {
                    kingCantMoveHere.Add(new NormalOrSpecialMove(j));
                    if ((j.gameObject.transform.childCount == 0) || (this.isWhite != j.PieceInSquare.isWhite))
                    {
                        validMoves.Add(new NormalOrSpecialMove(j));
                    }
            }
        }












    }

}


