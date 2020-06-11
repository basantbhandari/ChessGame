using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnToBBishopOnClick : PawnToPieceOnClick
{
    public override void ReplacePawn(Square LocationOfPawn, int PreviousMovesMade)
    {
        Bishop AddedPiece = LocationOfPawn.GetComponentInChildren<Canvas>().gameObject.AddComponent<Bishop>();
        AddedPiece.Initialize(false, PreviousMovesMade);
 
    }


}
