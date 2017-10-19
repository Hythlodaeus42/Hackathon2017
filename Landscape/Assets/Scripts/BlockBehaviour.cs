using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    private bool selected = false;
    private Behaviour halo;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSelect()
    {
        Debug.Log("NodeBehaviour.OnSelect() called");

        // set selected
        selected = !selected;

        halo = (Behaviour)this.GetComponent("Halo");
        halo.enabled = selected;
    }
}