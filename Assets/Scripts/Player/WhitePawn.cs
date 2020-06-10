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
                      (this.movesMode == 0) &&
                      (
                         (j.indRow == (this.indRow + 2)) && (j.indCol == this.indCol)
                      ) 
                      &&
                      (
                         TheCanvas.AllSquares[this.indRow + 1, this.indCol].gameObject.transform.childCount == 0
                      )
                  ) 
                  && 
                  (
                     j.gameObject.transform.childCount == 0
                  )
               )
            {
                validMoves.Add(new NormalOrSpecialMove(j));
            }
            else if (
                   // TODO check one more time this condition for en passend
                   (j.indRow == 5) &&
                   (
                       (TheCanvas.AllSquares[j.indRow - 1, j.indCol].gameObject.transform.childCount != 0 )           &&
                       (TheCanvas.AllSquares[j.indRow - 1, j.indCol].PieceInSquare is BlackPawn           )           &&
                       (j.indRow == this.indRow + 1                                                       )           &&
                       (
                         (j.indCol == this.indCol - 1) || 
                         (j.indCol == this.indCol + 1)
                       ) 
                       &&
                       (TheCanvas.LastPieceMoved == TheCanvas.AllSquares[j.indRow - 1, j.indCol].PieceInSquare)
                   )
                )
                {
                    validMoves.Add(new NormalOrSpecialMove(j, true));
                }
            else if (
                        // for move after first white pawn move
                        (
                            ((j.indRow == (this.indRow + 1)) && (j.indCol == (this.indCol - 1)) && (j.gameObject.transform.childCount != 0)) ||
                            ((j.indRow == (this.indRow + 1)) && (j.indCol == (this.indCol + 1)) && (j.gameObject.transform.childCount != 0)) ||
                            ((j.indRow == (this.indRow + 1)) && (j.indCol == (this.indCol    )) && (j.gameObject.transform.childCount != 0)) ||
                            ((j.indRow == (this.indRow + 1)) && (j.indCol == (this.indCol    )) && (j.gameObject.transform.childCount == 0))
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


