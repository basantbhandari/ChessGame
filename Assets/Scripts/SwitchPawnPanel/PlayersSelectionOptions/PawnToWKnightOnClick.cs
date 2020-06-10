using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PawnToWKnightOnClick : PawnToPieceOnClick
{
    public override void ReplacePawn(Square LocationOfPawn)
    {

        RemovePawn(LocationOfPawn);

        Knight AddedPiece = LocationOfPawn.GetComponentInChildren<Canvas>().gameObject.AddComponent<Knight>();
        AddedPiece.Initialize(true);
        LocationOfPawn.PieceInSquare.gameObject.GetComponentInChildren<RawImage>().texture = this.PieceTexture;

        thePieceColorController.ReturningToNormal();
    }


}
