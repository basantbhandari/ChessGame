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
                    (( j.indRow - this.indRow ) ==  ( j.indCol - this.indCol )) || 
                    (( j.indRow - this.indRow ) == -( j.indCol - this.indCol ))
                )
               )
            {
                validMoves.Add(new NormalOrSpecialMove(j));

                if (j.gameObject.transform.childCount != 0)
                {
                    if (j.indRow < this.indRow && j.indCol > this.indCol)
                    {
                            checkDownRightSquare = j;   
                    }
                    else if (j.indRow < this.indRow && j.indCol < this.indCol)
                    {
                            checkDownLeftSquare = j;
                    }
                    else if (j.indRow > this.indRow && j.indCol < this.indCol)
                    {
                        if (checkUpLeftSquare == null)
                        {
                            checkUpLeftSquare = j;
                        }
                    }
                    else if (j.indRow > this.indRow && j.indCol > this.indCol)
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
