using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementSelector : MonoBehaviour
{
    [SerializeField] private Text SelectedElementText;
    [SerializeField] private List<Button> ButtonList;
    [SerializeField] private Element SelectedElement;

    public void ChangeSelectedElement(Element selectedElement)
    {
        SelectedElement = selectedElement;
        SelectedElementText.text = $"CURRENTLY SELECTED: {selectedElement.GetElementName()}";

        // TODO: Save SelectedElement to a player config, scriptableobject?
    }


    public void EnableAllElementButtons()
    {
        foreach(Button button in ButtonList)
            button.interactable = true;
    }


    public void DisableAllElementButtons()
    {
        foreach(Button button in ButtonList)
            button.interactable = false;
    }
}
