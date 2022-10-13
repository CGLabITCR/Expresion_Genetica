using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConstructPieceElement : MonoBehaviour
{
    // Start is called before the first frame update
    public ElementTypeEnum _tipoElemento;
    public ElementNameEnum _nombreElemento;

    void Start()
    {
        
    }

    private void Awake()
    {
        UnitManager.Instance.addConstructPiece(this.GetComponent<ConstructPieceElement>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
