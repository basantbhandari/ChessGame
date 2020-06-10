using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Queen : Piece
{
                // Queen is combination of Rook and Bishop
    public override void CheckValidMoves()
    {
        // for rook
        Square checkUpSquare = null;
        Square checkRightSquare = null;
        Square checkDownSquare = null;
        Square checkLeftSquare = null;

        // for bishop
        Square checkUpLeftSquare = null;
        Square checkUpRightSquare = null;
        Square checkDownRightSquare = null;
        Square checkDownLeftSquare = null;

        validMoves.Clear();

        foreach (Square j in TheCanvas.AllSquares)
        {
            if (
                       // rook
                        (
                                  (
                                    ( this.indRow == j.indRow ) || 
                                    ( this.indCol == j.indCol )
                                   )
                        )       
                        ||
                        // bishop
                        (  
                                  (
                                    (j.indRow - this.indRow) ==  (j.indCol - this.indCol)
                                  )
                                  ||
                                  (
                                    (j.indRow - this.indRow ) == -(j.indCol - this.indCol)
                                  )
                        )
               )
            {
                validMoves.Add(new NormalOrSpecialMove(j));

                if (j.gameObject.transform.childCount != 0)
                {

                           // for rook property
                            if (j.indRow < this.indRow && this.indCol == j.indCol)
                            {
                                checkDownSquare = j;
                            }
                            else if (j.indCol < this.indCol && this.indRow == j.indRow)
                            {
                                checkLeftSquare = j;
                            }
                            else if (j.indCol > this.indCol && this.indRow == j.indRow)
                            {
                                if (checkRightSquare == null)
                                {
                                    checkRightSquare = j;
                                }
                            }
                            else if (j.indRow > this.indRow && this.indCol == j.indCol)
                            {
                                if (checkUpSquare == null)
                                {
                                    checkUpSquare = j;
                                }
                            }
                            // for bishop property
                            else if (j.indRow < this.indRow && j.indCol > this.indCol)
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










        //copy of valid move for up ,right square OR  for  downleft, downright square
        List<NormalOrSpecialMove> validMoveCopy = new List<NormalOrSpecialMove>();


        foreach (NormalOrSpecialMove j in validMoves)
        {
            validMoveCopy.Add(j);
        }









        // working on down, left square on the way OR working on upleft , upright square on the way   
        foreach (NormalOrSpecialMove j in validMoveCopy)
        {
            if (
                (
                    ( j.theValidMove.gameObject.transform.childCount != 0 && j.theValidMove.PieceInSquare.isWhite == this.isWhite                       )  ||
                    ( checkRightSquare != null && j.theValidMove.indCol > checkRightSquare.indCol && j.theValidMove.indRow  == checkRightSquare.indRow  )  ||
                    ( checkUpSquare != null && j.theValidMove.indRow > checkUpSquare.indRow && j.theValidMove.indCol == checkUpSquare.indCol            )
                )
                ||
                (
                    ( checkUpLeftSquare  != null && (j.theValidMove.indRow > checkUpLeftSquare.indRow  && j.theValidMove.indCol < checkUpLeftSquare.indCol  )) ||
                    ( checkUpRightSquare != null && (j.theValidMove.indRow > checkUpRightSquare.indRow && j.theValidMove.indCol > checkUpRightSquare.indCol ))
                )
              )
            {
                validMoves.Remove(j);
            }
        }













        // working on down, left square on the way OR working on downLeft, DownRight square on the way
        validMoveCopy.Reverse();

        // for rook
        bool foundLeft = false;
        bool foundDown = false;
        // for bishop
        bool foundDownLeft = false;
        bool foundDownRight = false;

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
                else if (checkDownLeftSquare != null && j.theValidMove == checkDownLeftSquare)
                {
                    foundDownLeft = true;
                }
                else if (checkDownRightSquare != null && j.theValidMove == checkDownRightSquare)
                {
                    foundDownRight = true;
                }



                // for rook
                if (
                       (
                            (foundLeft && (j.theValidMove.indCol < checkLeftSquare.indCol && j.theValidMove.indRow == checkLeftSquare.indRow ))   ||
                            (foundDown && (j.theValidMove.indRow < checkDownSquare.indRow && j.theValidMove.indCol == checkDownSquare.indCol ))
                       )
                   )
                {
                    validMoves.Remove(j);
                }

                // for bishop
                if (
                        (foundDownLeft  && (j.theValidMove.indRow < checkDownLeftSquare.indRow  && j.theValidMove.indCol < checkDownLeftSquare.indCol  )) ||
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


