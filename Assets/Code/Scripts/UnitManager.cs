using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public enum ElementTypeEnum
{
    Enzima,
    Start,
    Promotor,
    Enhancer,
    Gen_de_Interes,
    Gen_Reportero,
    Terminador,
    Promotor_2,
    Gen_de_Seleccion,
    Stop,
    Enzima_2
}

public enum ElementNameEnum
{
    //Enzima 
    Xbal,
    Notl,
    //Start
    TAA,
    //Promotores
    CaMV_35S,
    sab,
    PLOCSn,
    sba,
    //Promoto Gen de Seleccion
    NOS,
    //Enhancer
    RBCS2,
    RBCS1_i_2,
    //Gen de Interes
    HLF1,
    //Genes Reporteros 
    GFP,
    Luc,
    Gus,
    LacZ,
    //Terminadores
    _3_PLOCSn,
    _3_CamMV_35S,
    _3_sab,
    _3_sba,
    //Gen de Seleccion
    Nptll,
    //Stop 
    ATG
}



public class UnitManager : MonoBehaviour
{


    public static UnitManager Instance;
    private List<ConstructBoardBox> _boardElementBoxes = new List<ConstructBoardBox>();
    private List<ConstructPieceElement> _boardElementPieces = new List<ConstructPieceElement>();

    private Dictionary<ElementTypeEnum, ElementNameEnum> CorrectConstructElements = new Dictionary<ElementTypeEnum, ElementNameEnum>
    {
        {ElementTypeEnum.Enzima, ElementNameEnum.Notl},
        {ElementTypeEnum.Start, ElementNameEnum.ATG},
        {ElementTypeEnum.Promotor, ElementNameEnum.CaMV_35S},
        {ElementTypeEnum.Enhancer, ElementNameEnum.RBCS1_i_2},
        {ElementTypeEnum.Gen_de_Interes, ElementNameEnum.HLF1},
        {ElementTypeEnum.Gen_Reportero, ElementNameEnum.GFP},
        {ElementTypeEnum.Terminador, ElementNameEnum._3_CamMV_35S},

        {ElementTypeEnum.Promotor_2, ElementNameEnum.NOS},
        {ElementTypeEnum.Gen_de_Seleccion, ElementNameEnum.Nptll},
        {ElementTypeEnum.Stop, ElementNameEnum.ATG},
        {ElementTypeEnum.Enzima_2, ElementNameEnum.Xbal},
    };


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void addConstructBox(ConstructBoardBox newElementBox)
    {

        if(!newElementBox.CompareTag("Untagged"))
        {
            /* 
            foreach( GameObject elemento in _boardElements)
            {
                if (elemento.CompareTag(nuevoElemento.tag)){
                    CanvasManager.Instance.sendNotification("Ya esta en la lista", 2);
                    nuevoElemento = elemento;
                }
            }
            */
            _boardElementBoxes.Add(newElementBox);
            string tipoElemento = newElementBox._tipoElemento.ToString();
            CanvasManager.Instance.sendNotification("Nueva  Casilla de tipo: " + tipoElemento, 3);
        }
        
    }

    public void addConstructPiece(ConstructPieceElement newElementPiece)
    {
        if (!newElementPiece.CompareTag("Untagged"))
        {
            _boardElementPieces.Add(newElementPiece);
            string tipoElemento = newElementPiece._tipoElemento.ToString();
            string nombreElemento = newElementPiece._nombreElemento.ToString();

            CanvasManager.Instance.sendNotification("Nueva PIEZA: " + nombreElemento + " de tipo: " + tipoElemento, 3);

        }
    }

    public void boardBoxCollision( ConstructBoardBox box, Collider other)
    {
        ConstructPieceElement boardPiece = other.GetComponent<ConstructPieceElement>();
        
        //Revizamos si el otro componente es una Pieza 
        if( boardPiece != null)
        {

            //Verificamos que esta en la lista 
            var checkBoardPiece = _boardElementPieces.Find(piece => piece.Equals(boardPiece));
            var checkBoardBox = _boardElementBoxes.Find(bx => bx.Equals(box));

            if( checkBoardBox != null && checkBoardPiece != null)
            {   //Verificamos el tipo
                if( box._tipoElemento == boardPiece._tipoElemento)
                {
                    if(CorrectConstructElements[box._tipoElemento] == boardPiece._nombreElemento)
                    {
                        CanvasManager.Instance.sendNotification("BC-Son del mismo tipo y mismo elemento", 2);
                        SoundManager.Instance.PlaySuccessSound();
                        box.changeState("success");
                        //Pintelo de Verde
                    }
                    else
                    {
                        CanvasManager.Instance.sendNotification("BC-Son del mismo tipo y diferente elemento", 2);
                        SoundManager.Instance.PlayFailureSound();
                        box.changeState("warning");
                        //Pintelo de Amarillo
                    }

                }
                else
                {
                    //Pinta el box de color rojo 
                    CanvasManager.Instance.sendNotification("BC-Son de tipos distintos", 2);
                    SoundManager.Instance.PlayFailureSound();
                    box.changeState("failure");


                }

            }else
            {
                CanvasManager.Instance.sendNotification("BC-No se encontraron las unidades", 2);

            }

        }
        else
        {
            CanvasManager.Instance.sendNotification("BC-No se reconocio la pieza", 2);

        }





    }



    public void showLenghtNotification()
    {
        int lenElementos = _boardElementBoxes.Count;

        if(lenElementos > 0)
        {
            string elementos = "";
            foreach (ConstructBoardBox box in _boardElementBoxes)
            {
                elementos += "--" + box._tipoElemento;
            }

            string listLen = "Hay " +lenElementos +" elementos. Son: " + elementos;

            CanvasManager.Instance.sendNotification(listLen, 5);
        }
        else
        {
            CanvasManager.Instance.sendNotification("No hay elementos en la lista", 5);
        }
        
    }

}
