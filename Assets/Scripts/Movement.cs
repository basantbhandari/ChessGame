using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Movement
{
    public int onTurnNumber { get; set; }
    public Square MovedFrom { get; set; }
    public Piece PieceMoved { get; set; }
    public NormalOrSpecialMove MovedTo;
    public Piece CapturedPiece { get; set; }



    public Movement(int inpOnTurnNumber, Square inpMovedFrom, Piece inpPieceMoved, NormalOrSpecialMove inpMovedTo, Piece inpCapturedPiece = null)
    {
        onTurnNumber = inpOnTurnNumber;
        MovedFrom = inpMovedFrom;
        PieceMoved = inpPieceMoved;
        MovedTo = inpMovedTo;
        CapturedPiece = inpCapturedPiece;
    
    }







}
