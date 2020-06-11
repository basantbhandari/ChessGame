using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PawnToPieceOnClick : MonoBehaviour, IPointerDownHandler
{

    private Square LocationOfPawn = null;
    public Texture PieceTexture;
    public PieceColorDisplayController thePieceColorController;

    public void SetLocationOfPawn(Square inpLocationOfPawn)
    {
        LocationOfPawn = inpLocationOfPawn;
    }


    public void RemovePawn(Square RemoveFromHere)
    {
         DestroyImmediate(RemoveFromHere.PieceInSquare);
         RemoveFromHere.PieceInSquare = null;
    
    }

    public virtual void ReplacePawn(Square LocationOfPawn)
      { 
          RemovePawn(LocationOfPawn);
          thePieceColorController.ReturningToNormal();
      }




      public void OnPointerDown(PointerEventData eventData)
      {
          /*     RemovePawn(LocationOfPawn);
               ReplacePawn(LocationOfPawn);
               LocationOfPawn.PieceInSquare.gameObject.GetComponentInChildren<RawImage>().texture = this.PieceTexture;
               thePieceColorController.ReturningToNormal();*/


        RemovePawn(LocationOfPawn);
        ReplacePawn(LocationOfPawn);
        
        LocationOfPawn.PieceInSquare.gameObject.GetComponentInChildren<RawImage>().texture = this.PieceTexture;
        thePieceColorController.ReturningToNormal();

    }














}
