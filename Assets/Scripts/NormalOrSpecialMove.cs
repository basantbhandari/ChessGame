using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalOrSpecialMove 
{
    public Square theValidMove { get; set; }
    public bool isSpecial { get; set; }
    public bool canTakePiece { get; set; }

    public NormalOrSpecialMove(Square theMove, bool isMoveSpecial = false, bool canTakePiece = true)
    {
        theValidMove = theMove;
        isSpecial = isMoveSpecial;
    }
}
