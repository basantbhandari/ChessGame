using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WhitePawn : Piece
{

    public override void CheckValidMoves()
    {
        validMoves.Clear();
        foreach (Square j in TheCanvas.AllSquares)
        {
            if (
                  // for first white pawn move
                  (
                      (this.movesMade == 0) &&
                      (
                         (j.indRow == (this.SquareOfPiece.indRow + 2)) && (j.indCol == this.SquareOfPiece.indCol)
                      )
                      &&
                      (
                         TheCanvas.AllSquares[this.SquareOfPiece.indRow + 1, this.SquareOfPiece.indCol].gameObject.transform.childCount == 0
                      )

                  )
                  &&
                  (
                     j.gameObject.transform.childCount == 0
                  )
               )
            {
                validMoves.Add(new NormalOrSpecialMove(j, false, false));
            }
            else if (
                   // TODO check one more time this condition for en passend
                   (j.indRow == 5) &&
                   (
                       (TheCanvas.AllSquares[j.indRow - 1, j.indCol].gameObject.transform.childCount != 0) &&
                       (TheCanvas.AllSquares[j.indRow - 1, j.indCol].PieceInSquare is BlackPawn) &&
                       (j.indRow == this.SquareOfPiece.indRow + 1) &&
                       (
                         (j.indCol == this.SquareOfPiece.indCol - 1) ||
                         (j.indCol == this.SquareOfPiece.indCol + 1)
                       )


                       &&
                       (TheCanvas.allMoves[TheCanvas.allMoves.Count - 1].PieceMoved == TheCanvas.AllSquares[j.indRow - 1, j.indCol].PieceInSquare)
                   )
                )
            {
                validMoves.Add(new NormalOrSpecialMove(j, true));
            }
            else if (
                        // for move after first white pawn move
                        (


                            ((j.indRow == (this.SquareOfPiece.indRow + 1)) && (j.indCol == (this.SquareOfPiece.indCol - 1)) && (j.gameObject.transform.childCount != 0)) ||
                            ((j.indRow == (this.SquareOfPiece.indRow + 1)) && (j.indCol == (this.SquareOfPiece.indCol + 1)) && (j.gameObject.transform.childCount != 0)) ||
                            ((j.indRow == (this.SquareOfPiece.indRow + 1)) && (j.indCol == (this.SquareOfPiece.indCol)) && (j.gameObject.transform.childCount != 0)) ||
                            ((j.indRow == (this.SquareOfPiece.indRow + 1)) && (j.indCol == (this.SquareOfPiece.indCol)) && (j.gameObject.transform.childCount == 0))


                        )
                )
            {
                if ((j.gameObject.transform.childCount == 0) || (this.isWhite != j.PieceInSquare.isWhite))
                {
                    if (j.indRow != 7)
                    {
                        validMoves.Add(new NormalOrSpecialMove(j));
                    }
                    else
                    {
                        // TODO change letter on  , pawn on other side  called pawn promossion
                        validMoves.Add(new NormalOrSpecialMove(j, true));
                    }
                }

            }
            else if (
                        (j.indRow == (this.SquareOfPiece.indRow + 1)) &&
                        (j.indCol == this.SquareOfPiece.indCol)       &&
                        (j.gameObject.transform.childCount == 0)
                 ) 
                 {
                     validMoves.Add(new NormalOrSpecialMove(j, false, false));
                 }

        }




    }
}


