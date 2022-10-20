using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestTube : MonoBehaviour
{


    private Material original_trigger_material;
    private Color _successColor = new Color(0f, 1f, 0f, 0.5f);
    private Color _failureColor = new Color(1f, 0f, 0f, 0.5f);


    public bool _isConstructSynthesized = false;

    // Start is called before the first frame update
    void Start()
    {
        UnitManager.Instance.addSynthesized(this.GetComponent<TestTube>());

    }

    public void synthConstruct()
    {
        original_trigger_material = this.GetComponent<Renderer>().material;
        _isConstructSynthesized = true;
        original_trigger_material.color = _successColor;
        CanvasManager.Instance.sendNotification("El Constructo se ha sintetizado", 3);
    }

    public void empty()
    {
        original_trigger_material = this.GetComponent<Renderer>().material;
        _isConstructSynthesized = false;
        original_trigger_material.color = _failureColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
