using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Plasmid : MonoBehaviour
{
    

    public bool _isCorrect = false;

    Material original_liquid_material;
    private Color original_liquid_color;
    private Color _successColor = new Color(0.589f, 0.99f, 1f, 0.66f);


    public GameObject _tobaccoPlant;
    public GameObject _PlasmidLiquid;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //Instancia el Plasmido (Placa Petri)
        UnitManager.Instance.addPlasmid(this.GetComponent<Plasmid>());
        //Guarda el color y el material del liquido del plasmido 
        original_liquid_material = this._PlasmidLiquid.GetComponent<Renderer>().material;
        original_liquid_color = this._PlasmidLiquid.GetComponent<Renderer>().material.color;


        //Instancia la planta de Tabaco
        var plant = Instantiate(_tobaccoPlant, this.transform.position, Quaternion.AngleAxis(-360, Vector3.right), this.transform);
        _tobaccoPlant = plant;
        this._tobaccoPlant.SetActive(false); //La oculta
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        var testTube = other.GetComponent<TestTube>();
        if (testTube != null)   
        {
            if (testTube._isConstructSynthesized)
            {
                changeState("vector");
            }
            else
            {
                changeState("failure");
            }
        }
        
    }

    public void changeState(string state)
    {
        original_liquid_material = this._PlasmidLiquid.GetComponent<Renderer>().material;

        switch (state)
        {
            case "vector":
         
                _isCorrect = true;

                //Cambio el color del liquido
                original_liquid_material.color = _successColor;

                //Activo la Planta
                this._tobaccoPlant.SetActive(true);
                break;
            case "failure":
                _isCorrect = false;
                //Pinto el liquido del color original
                original_liquid_material.color = original_liquid_color;
                //Escondo la planta
                this._tobaccoPlant.SetActive(false);
                break;
            default:
                original_liquid_material.color = original_liquid_color;
                break;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //original_trigger_material = this.GetComponent<Renderer>().material;
        original_liquid_material.color = original_liquid_color;
        _tobaccoPlant.SetActive(false);
    }
}
