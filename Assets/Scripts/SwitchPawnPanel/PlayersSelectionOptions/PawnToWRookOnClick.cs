using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnToWRookOnClick : PawnToPieceOnClick
{

    public override void ReplacePawn(Square LocationOfPawn, int PreviousMovesMade)
    {
        Rook AddedPiece = LocationOfPawn.GetComponentInChildren<Canvas>().gameObject.AddComponent<Rook>();
        AddedPiece.Initialize(true, PreviousMovesMade);
 
    }



}
