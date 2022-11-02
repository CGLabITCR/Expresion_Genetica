using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class ConstructBoardBox : MonoBehaviour
{
    Material original_trigger_material;

    public ElementTypeEnum _tipoElemento;
    public bool _isCorrect = false;

    private Color _warningColor = new Color(1f, 1f, 0f, 0.5f);     //Color Amarillo
    private Color _successColor = new Color(0f, 1f, 0f, 0.5f);
    private Color _failureColor = new Color(1f, 0f, 0f, 0.5f);
    private Color _emptyColor   = new Color(0f, 0f, 1f, 0.5f);

    public GameObject _floatingTextPrefab;

    public GameObject _successPrefab;
    public GameObject _failurePrefab;

    private GameObject _success;
    private GameObject _failure;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        //Guarda la unidad en el Unit Manager 
        UnitManager.Instance.addConstructBox( this.GetComponent<ConstructBoardBox>() );
        //Muestra una notificacion de la cantidad de elementos de BoardBox dentro del unit manager
        UnitManager.Instance.showLenghtNotification();

       _success = Instantiate(_successPrefab, this.transform.position, Quaternion.identity, this.transform);
       _failure = Instantiate(_failurePrefab, this.transform.position, Quaternion.identity, this.transform);

        _successPrefab = _success;
        _failurePrefab = _failure;

        _successPrefab.SetActive(false);
        _failurePrefab.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        /**
        original_trigger_material = this.GetComponent<Renderer>().material;

        //Si son del mismo tipo (Promotor, Enzima....) entonces reviza si es correcto
        if (other.gameObject.CompareTag(this.gameObject.tag))
        {
            original_trigger_material.color =  new Color(0f, 1f, 0f, 0.5f);

            CanvasManager.Instance.sendNotification("Correcto", 3);
            SoundManager.Instance.PlaySuccessSound();
            
        }
        //Si son de tipos diferentes, entonces lo pinta rojo
        else {
            original_trigger_material.color = new Color(1f, 0f, 0f, 0.5f);
            CanvasManager.Instance.sendNotification("Incorrecto", 3);
            SoundManager.Instance.PlayFailureSound();
        }
        */


        //Manda a verificar la colision
        UnitManager.Instance.boardBoxCollision(this.GetComponent<ConstructBoardBox>(), other);
    }

    private void OnTriggerExit(Collider other)
    {
        //Lo pinta de color azulito
        original_trigger_material = this.GetComponent<Renderer>().material;
        original_trigger_material.color = _emptyColor;

        _failurePrefab.SetActive(false);
        _successPrefab.SetActive(false);
    }

    public void changeState( string state)
    {
        original_trigger_material = this.GetComponent<Renderer>().material;

        switch (state)
        {
            case "success":
                //Me pinto de Color Verde
                _isCorrect = true;
                original_trigger_material.color = _successColor;
                _failurePrefab.SetActive(false);
                _successPrefab.SetActive(true);
                break;
            case "warning":
                //Me pinto de Color Amarillo 
                _isCorrect = false;
                original_trigger_material.color = _warningColor;

                _failurePrefab.SetActive(true);
                _successPrefab.SetActive(false);

                if (_floatingTextPrefab)
                {
                    showFloatingText("Mensaje de Warning");
                }
                  
                break;
            case "failure":
                _isCorrect = false;
                original_trigger_material.color = _failureColor;
                _failurePrefab.SetActive(true);
                _successPrefab.SetActive(false);
                //Me pinto de Color Rojo 
                break;

            default:
                // No hace nada 
                break;
        }
    }

    public void showFloatingText(string text)
    {
        var floatingText = Instantiate(_floatingTextPrefab, this.transform.position, Quaternion.identity, this.transform);
        floatingText.GetComponent<TextMeshPro>().text = text;

    }


}
