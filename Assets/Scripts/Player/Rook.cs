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

        foreach (Square j in TheCanvas.AllSquares)
        {
            if (((this.indRow == j.indRow) || (this.indCol == j.indCol)))
            {
                validMoves.Add(new NormalOrSpecialMove(j));

                if (j.gameObject.transform.childCount != 0)
                {
                    if (j.indRow < this.indRow)
                    {
                        checkDownSquare = j;
                    }
                    else if (j.indCol < this.indCol)
                    {
                        checkLeftSquare = j;
                    }
                    else if (j.indCol > this.indCol)
                    {
                        if (checkRightSquare == null)
                        {
                            checkRightSquare = j;
                        }
                    }
                    else if (j.indRow > this.indRow)
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
            }
        }





        // working on down, left square on the way
        validMoveCopy.Reverse();

        bool foundLeft = false;
        bool foundDown = false;

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
                    (foundLeft && (j.theValidMove.indCol < checkLeftSquare.indCol))  ||
                    (foundDown && (j.theValidMove.indRow < checkDownSquare.indRow))
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




