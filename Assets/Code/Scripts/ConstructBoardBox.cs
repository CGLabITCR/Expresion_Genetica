using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Funcion generate arrow gracias a: https://www.youtube.com/watch?v=8lXDLy24rJw
//Codigo: https://pastebin.com/HmrYF0xr

public class ConstructBoardBox : MonoBehaviour
{
    Material original_trigger_material;

    public ElementTypeEnum _tipoElemento;

    private bool _isCorrect = false;
    private bool _isChecked = false;

    //Cambio de la forma a Arrow 
    public float stemLength;
    public float stemWidth ;
    public float tipLength;
    public float tipWidth;
    private float _centerOffSet;

    [System.NonSerialized]
    private List<Vector3> verticesList;
    [System.NonSerialized]
    private List<int> trianglesList;
    private Mesh mesh;


    //Colores 
    private Color _warningColor = new Color(1f, 1f, 0f, 0.5f);     //Color Amarillo
    private Color _successColor = new Color(0f, 1f, 0f, 0.5f);
    private Color _failureColor = new Color(1f, 0f, 0f, 0.5f);
    private Color _emptyColor   = new Color(0f, 0f, 1f, 0.5f);

    public GameObject _floatingTextPrefab;
    public GameObject _successPrefab;
    public GameObject _failurePrefab;


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

        mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;
        GenerateArrow();

        _centerOffSet = (stemLength + tipLength) / 2;
        var offset = this.transform.position + (this.transform.right * _centerOffSet);
        
        var _success = Instantiate(_successPrefab, offset, Quaternion.identity, this.transform);
        var _failure = Instantiate(_failurePrefab, offset, Quaternion.identity, this.transform);

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
                _isChecked = true;
                original_trigger_material.color = _successColor;
                _failurePrefab.SetActive(false);
                _successPrefab.SetActive(true);
                break;

            case "warning":
                //Me pinto de Color Amarillo 
                _isCorrect = false;
                _isChecked = true;
                original_trigger_material.color = _warningColor;

                _failurePrefab.SetActive(true);
                _successPrefab.SetActive(false);

                if (_floatingTextPrefab)
                {
                    var typeStr = _tipoElemento.ToString();
                    var isWrong = " Incorrecto";
                    typeStr.Replace("_2", "");

                    if (typeStr == "Enzima")
                    {
                        isWrong = " Incorrecta";
                    }

                    showFloatingText(typeStr + isWrong);
                }
                  
                break;
            case "failure":
                _isCorrect = false;
                _isChecked = true;
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
        var offset = this.transform.position + (this.transform.right * _centerOffSet) + (this.transform.up * 0.025f);
        var floatingText = Instantiate(_floatingTextPrefab, offset, Quaternion.identity, this.transform);
        floatingText.GetComponent<TextMeshPro>().text = text;

    }


    private void GenerateArrow()
    {
        //setup
        verticesList = new List<Vector3>();
        trianglesList = new List<int>();

        //stem setup
        Vector3 stemOrigin = Vector3.zero;
        float stemHalfWidth = stemWidth / 2f;
        //Stem points
        verticesList.Add(stemOrigin + (stemHalfWidth * Vector3.back));
        verticesList.Add(stemOrigin + (stemHalfWidth * Vector3.forward));
        verticesList.Add(verticesList[0] + (stemLength * Vector3.right));
        verticesList.Add(verticesList[1] + (stemLength * Vector3.right));

        //Stem triangles
        trianglesList.Add(0);
        trianglesList.Add(1);
        trianglesList.Add(3);

        trianglesList.Add(0);
        trianglesList.Add(3);
        trianglesList.Add(2);

        //tip setup
        Vector3 tipOrigin = stemLength * Vector3.right;
        float tipHalfWidth = tipWidth / 2;

        //tip points
        verticesList.Add(tipOrigin + (tipHalfWidth * Vector3.forward));
        verticesList.Add(tipOrigin + (tipHalfWidth * Vector3.back));
        verticesList.Add(tipOrigin + (tipLength * Vector3.right));

        //tip triangle
        trianglesList.Add(4);
        trianglesList.Add(6);
        trianglesList.Add(5);

        //assign lists to mesh.
        mesh.vertices = verticesList.ToArray();
        mesh.triangles = trianglesList.ToArray();
        
    }


    public bool getIsCorrect()
    {
        return this._isCorrect;
    }

    public bool getIsChecked()
    {
        return this._isChecked;
    }

}
