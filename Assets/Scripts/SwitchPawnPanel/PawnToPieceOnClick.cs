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


    public int RemovePawn(Square RemoveFromHere)
    {
        int movesMadeByPawn = RemoveFromHere.PieceInSquare.movesMade;
         DestroyImmediate(RemoveFromHere.PieceInSquare);
         RemoveFromHere.PieceInSquare = null;


        return movesMadeByPawn;
    }

    public virtual void ReplacePawn(Square LocationOfPawn, int PreviousMovesMade)
      { 
          RemovePawn(LocationOfPawn);
          thePieceColorController.ReturningToNormal();
      }




      public void OnPointerDown(PointerEventData eventData)
      {
        int movesMadeByPawn = RemovePawn(LocationOfPawn);
        ReplacePawn(this.LocationOfPawn, movesMadeByPawn);
        
        LocationOfPawn.PieceInSquare.gameObject.GetComponentInChildren<RawImage>().texture = this.PieceTexture;
        thePieceColorController.ReturningToNormal();

      }














}
