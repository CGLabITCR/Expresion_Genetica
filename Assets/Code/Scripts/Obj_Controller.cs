using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Obj_Controller : MonoBehaviour
{
    Material original_trigger_material;


    // Start is called before the first frame update
    void Start()
    {
        UnitManager.Instance.inserElement(this.gameObject);
        UnitManager.Instance.showLenghtNotification();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
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
        
    }

    private void OnTriggerExit(Collider other)
    {
        original_trigger_material.color = new Color(0f, 0f, 1f, 0.5f);
    }


}
