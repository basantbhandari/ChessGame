using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnToWKnightOnClick : PawnToPieceOnClick
{
    public override void ReplacePawn(Square LocationOfPawn, int PreviousMovesMade)
    {
        Knight AddedPiece = LocationOfPawn.GetComponentInChildren<Canvas>().gameObject.AddComponent<Knight>();
        AddedPiece.Initialize(true);
 
    }


}
