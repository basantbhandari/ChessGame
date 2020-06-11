using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnToWRookOnClick : PawnToPieceOnClick
{

    public override void ReplacePawn(Square LocationOfPawn)
    {

        //RemovePawn(LocationOfPawn);

        Rook AddedPiece = LocationOfPawn.GetComponentInChildren<Canvas>().gameObject.AddComponent<Rook>();
        AddedPiece.Initialize(true);
       // LocationOfPawn.PieceInSquare.gameObject.GetComponentInChildren<RawImage>().texture = this.PieceTexture;
       // thePieceColorController.ReturningToNormal();

    }



}
