using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnToBQueenOnClick : PawnToPieceOnClick
{
    public override void ReplacePawn(Square LocationOfPawn)
    {

        //RemovePawn(LocationOfPawn);

        Queen AddedPiece = LocationOfPawn.GetComponentInChildren<Canvas>().gameObject.AddComponent<Queen>();
        AddedPiece.Initialize(true);
       // LocationOfPawn.PieceInSquare.gameObject.GetComponentInChildren<RawImage>().texture = this.PieceTexture;
       // thePieceColorController.ReturningToNormal();

    }


}
