using System;
using System.Collections;
using System.Collections.Generic;
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
            string tipoElemento = newElementBox._tipoElemento.ToString();
            CanvasManager.Instance.sendNotification("Nueva CASILLA: ", 3);
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
