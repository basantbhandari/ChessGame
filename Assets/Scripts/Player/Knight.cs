using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Knight : Piece
{
    public override void CheckValidMoves()
    {
        validMoves.Clear();
        foreach (Square j in TheCanvas.AllSquares)
        {
            if (
                    (
                        ((j.indRow == (this.indRow + 1)) && ((j.indCol == (this.indCol - 2)))) ||
                        ((j.indRow == (this.indRow + 2)) && ((j.indCol == (this.indCol - 1)))) ||
                        ((j.indRow == (this.indRow + 1)) && ((j.indCol == (this.indCol + 2)))) ||
                        ((j.indRow == (this.indRow + 2)) && ((j.indCol == (this.indCol + 1)))) ||
                        ((j.indRow == (this.indRow - 1)) && ((j.indCol == (this.indCol + 2)))) ||
                        ((j.indRow == (this.indRow - 2)) && ((j.indCol == (this.indCol + 1)))) ||
                        ((j.indRow == (this.indRow - 1)) && ((j.indCol == (this.indCol - 2)))) ||
                        ((j.indRow == (this.indRow - 2)) && ((j.indCol == (this.indCol - 1))))
                    )
             )
            {
                    if ((j.gameObject.transform.childCount == 0) || (this.isWhite != j.PieceInSquare.isWhite))
                    {
                        validMoves.Add(new NormalOrSpecialMove(j));
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


