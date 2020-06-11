using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PawnToWQueenOnClick : PawnToPieceOnClick
{
    public override void ReplacePawn(Square LocationOfPawn, int PreviousMovesMade)
    {
        Queen AddedPiece = LocationOfPawn.GetComponentInChildren<Canvas>().gameObject.AddComponent<Queen>();
        AddedPiece.Initialize(true, PreviousMovesMade);

    }


}
