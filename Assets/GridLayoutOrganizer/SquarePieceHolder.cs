using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class SquareStyle
{
    public Texture NewTexture;
    //public Piece PieceClass;
}


public class SquarePieceHolder : MonoBehaviour
{

    public static SquarePieceHolder Instance;

    public SquareStyle[] SquareStyles;

    void Awake()
    {
        Instance = this;
    }
}
