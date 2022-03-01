// Written by Kevin Chao

using UnityEngine;

// Base class for all menus, Singleton
public abstract class Menu<T> : Menu where T : Menu<T>
{
    // Property with public read-access, private write-access
    // Properties are treated like fields but can have behaviors execute on read/write
    // Static keyword means this property belongs to the Menu class rather than a specific
    // object. Enforces Singleton pattern, since there should only be one instance, but if
    // there are more for some reason, all of them will refer to the same property.
    public static T Instance
    {
        get;
        private set;
    }



    // Awake() and OnDestroy() are protected to still allow for their use by child classes,
    // virtual to allow for overloading and specifying functionality in children
    protected virtual void Awake()
    {
        // Set Instance to refer to current instance
        Instance = (T)this;
    }


    protected virtual void OnDestroy()
    {
        // Clear reference stored in Instance
        Instance = null;
    }


    public static void Open()
    {
        // If an instance of this menu does not already exist, create one
        if (Instance == null)
            MenuManager.Instance.CreateMenuInstance<T>();
            

        // If an instance of this menu exists, enable it
        else
            Instance.gameObject.SetActive(true);

        MenuManager.Instance.OpenMenu(Instance);
    }


    public static void Close()
    {
        if (Instance == null)
        {
            Debug.LogErrorFormat($"Cannot close {typeof(T)}, Instance is null");
            return;
        }

        MenuManager.Instance.CloseMenu(Instance);
    }
}



public abstract class Menu : MonoBehaviour
{

}