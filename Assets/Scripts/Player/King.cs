using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class King : Piece
{
    public override void CheckValidMoves()
    {
        validMoves.Clear();
        foreach (Square j in TheCanvas.AllSquares)
        {
            // condition for king valid movement
            if (
                    (
                      (((j.indCol - this.indCol) <= 1) && ((j.indCol - this.indCol) >= -1)) &&
                      (((j.indRow - this.indRow) <= 1) && ((j.indRow - this.indRow) >= -1))
                    )
               )
            {
                    if (
                            (j.gameObject.transform.childCount == 0 ) ||
                            (this.isWhite != j.PieceInSquare.isWhite)
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
                        (this.movesMode == 0)        &&
                        (j.indRow == this.indRow)    &&
                        (
                                // for queenside castling:   r _ _ _ k => _ _ k r _
                                (
                                    (TheCanvas.AllSquares[this.indRow, 0].PieceInSquare != null)             &&
                                    (TheCanvas.AllSquares[this.indRow, 0].PieceInSquare.movesMode == 0)      &&                                
                                    (j.indCol == 2)                                                          && 
                                    (TheCanvas.AllSquares[this.indRow, 1].PieceInSquare == null)             &&
                                    (TheCanvas.AllSquares[this.indRow, 2].PieceInSquare == null)             &&
                                    (TheCanvas.AllSquares[this.indRow, 3].PieceInSquare == null)
                                ) 
                                ||

                                // for kingside castling:    k _ _ r => _ r k _
                                (
                                    (TheCanvas.AllSquares[this.indRow, 7].PieceInSquare != null )            &&
                                    (TheCanvas.AllSquares[this.indRow, 7].PieceInSquare.movesMode == 0)      &&
                                    (j.indCol == 6)                                                          && 
                                    (TheCanvas.AllSquares[this.indRow, 5].PieceInSquare == null)             &&
                                    (TheCanvas.AllSquares[this.indRow, 6].PieceInSquare == null) 
                                 )
                        )
                   )
                {
                  validMoves.Add(new NormalOrSpecialMove(j, true));
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



