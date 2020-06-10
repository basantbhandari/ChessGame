﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PawnToWBishopOnClick : PawnToPieceOnClick
{
    public override void ReplacePawn(Square LocationOfPawn)
    {

        RemovePawn(LocationOfPawn);

        Bishop AddedPiece = LocationOfPawn.GetComponentInChildren<Canvas>().gameObject.AddComponent<Bishop>();
        AddedPiece.Initialize(true);
        LocationOfPawn.PieceInSquare.gameObject.GetComponentInChildren<RawImage>().texture = this.PieceTexture;

        thePieceColorController.ReturningToNormal();
    }


}
