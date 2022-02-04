using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour
{
    [SerializeField] 
    private int fieldState; //0 = ej plogad 1 = plogad 2 = sådd 3 = växt
    [SerializeField] 
    private GameObject[] fields;
    [SerializeField] 
    private ToolSwitch Tool;
    [SerializeField] 
    private Player p;

    private bool farmzone;
    private float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fieldState == 3)
        {
            timer += Time.fixedDeltaTime;
            
        }
        if (timer > 3)
        {
            ChangeFarmstate();
        }
        if (farmzone && NeededTool())
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ChangeFarmstate();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            farmzone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        farmzone = false;
    }

    public void ChangeFarmstate()
    {
        fieldState++;
        if (fieldState > 4)
        {
            fieldState = 0;
        }

        
        ChangeFieldObject();
    }

    private bool NeededTool()
    {
        if (fieldState == 0 && Tool.currentTool == 1)
        {
            return true;
        }
        if (fieldState == 1 && Tool.seed)
        {
            return true;
        }
        if (fieldState == 2 && Tool.currentTool == 4)
        {
            return true;
        }
        if (fieldState == 4 && Tool.currentTool == 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ChangeFieldObject()
    {
        
        for (int i = 0; i < fields.Length; i++)
        {
            if (i == fieldState)
            {
                fields[i].gameObject.SetActive(true);
            }
            else
            {
                fields[i].gameObject.SetActive(false);
            }
        }
    }
}