using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Plasmid : MonoBehaviour
{
    Material original_trigger_material;

    public bool _isCorrect = false;

    private Color _successColor = new Color(0f, 1f, 0f, 0.5f);
    private Color _failureColor = new Color(1f, 0f, 0f, 0.5f);

    public GameObject _tobaccoPlant;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        UnitManager.Instance.addPlasmid(this.GetComponent<Plasmid>());
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
        original_trigger_material = this.GetComponent<Renderer>().material;

        switch (state)
        {
            case "vector":
                //Me pinto de Color Verde
                _isCorrect = true;
                original_trigger_material.color = _successColor;
                break;
            case "failure":
                //Me pinto de Color Rojo 
                _isCorrect = false;
                original_trigger_material.color = _failureColor;
                break;
            default:
                // No hace nada 
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
