// Written by Kevin Chao

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ElementSelector : MonoBehaviour
{
    [SerializeField] private Text selectedElementText;
    [SerializeField] private List<Button> buttonList;
    private Element selectedElement;
    


    public Element GetSelectedElement()
    {
        if (selectedElement == null)
        {
            Debug.LogException(new Exception("No element selected."));
        }
        
        return selectedElement;
    }


    public void ChangeSelectedElement(Element selectedElement)
    {
        this.selectedElement = selectedElement;
        selectedElementText.text = $"CURRENTLY SELECTED: {selectedElement.GetElementName()}";
    }


    public void EnableAllElementButtons()
    {
        foreach (Button button in buttonList)
        {
            button.interactable = true;
        }
    }


    public void DisableAllElementButtons()
    {
        foreach (Button button in buttonList)
        {
            button.interactable = false;
        }
    }
}