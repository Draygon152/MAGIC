// Written by Kevin Chao

using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

// Manages creation of menus, Singleton
public class MenuManager : MonoBehaviour
{
    public MainMenu mainMenuPrefab;
    public OptionsMenu optionsMenuPrefab;
    public VideoOptions videoOptionsPrefab;
    public SoundOptions soundOptionsPrefab;
    public SingleplayerLobbyMenu singleplayerLobbyPrefab;
    public MultiplayerLobbyMenu multiplayerLobbyPrefab;
    public HUD hudPrefab;
    public PauseMenu pauseMenuPrefab;
    public VictoryGameOver victoryMenuPrefab;
    public DefeatGameOver defeatMenuPrefab;

    private Stack<Menu> menuStack = new Stack<Menu>();

    public static MenuManager Instance
    {
        get;
        private set;
    }



    // Fine to use private here, since nothing should inherit from MenuManager
    // and no other classes should be calling Awake() and OnDestroy()
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            MainMenu.Open();
        }
    }


    private void OnDestroy()
    {
        Instance = null;
    }


    // Creates instance of menu prefab
    public void CreateMenuInstance<T>() where T : Menu
    {
        // Find prefab object of supplied type
        T prefab = GetPrefab<T>();

        Instantiate(prefab, transform);
    }


    // Attempts to find and return prefab of menu that we wish to instantiate
    private T GetPrefab<T>() where T : Menu
    {
        // Grabs prefab fields of MenuManager class and stores in an array, using binding flags:
        //     Public: Includes public fields in search
        //     Instance: Includes instance methods
        //     DeclaredOnly: Only search fields declared in MenuManager, no inherited fields
        // https://docs.microsoft.com/en-us/dotnet/api/system.type.getfields?view=net-6.0
        FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Public |
                                                      BindingFlags.Instance |
                                                      BindingFlags.DeclaredOnly);

        foreach (FieldInfo field in fields)
        {
            // GetValue returns an object containing the value of the field belonging to the
            // class passed in as an argument, typecasted as type T
            // https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo.getvalue?view=net-6.0
            T prefab = field.GetValue(this) as T;

            // Check that prefab exists before returning
            if (prefab != null)
            {
                return prefab;
            }
                
        }

        throw new MissingReferenceException("Could not find prefab for menu: " + typeof(T));
    }


    public void OpenMenu(Menu openedMenu)
    {
        if (menuStack.Count > 0)
        {
            // Deactivate topmost menu if it exists
            menuStack.Peek().gameObject.SetActive(false);

            Canvas newTopCanvas = openedMenu.GetComponent<Canvas>();
            Canvas oldTopCanvas = menuStack.Peek().GetComponent<Canvas>();

            // Update the sorting order of the newly opened menu's canvas, effectively rendering order
            newTopCanvas.sortingOrder = oldTopCanvas.sortingOrder + 1;
        }

        // Add newly opened menu to menu stack
        menuStack.Push(openedMenu);
    }


    // Attempts to close requested menu type
    public void CloseMenu(Menu closedMenu)
    {
        if (menuStack.Count == 0)
        {
            Debug.LogErrorFormat(closedMenu, $"cannot close {closedMenu.GetType()}, menu stack empty");
            return;
        }

        if (menuStack.Peek() != closedMenu)
        {
            Debug.LogErrorFormat(closedMenu, $"cannot close {closedMenu.GetType()}, not at top of menu stack. {menuStack.Peek().GetType()} the top of the stack");
            return;
        }

        CloseTopMenu();
    }


    // Closes menu at top of menu stack
    private void CloseTopMenu()
    {
        Menu topMenu = menuStack.Pop();
        Destroy(topMenu.gameObject);

        // Reactivate menu that is now at top of stack if it exists
        if (menuStack.Count > 0)
        {
            menuStack.Peek().gameObject.SetActive(true);
        }
    }


    // Closes all open menus
    public void CloseAllMenus()
    {
        int openMenus = menuStack.Count;

        for (int i = 0; i < openMenus; i++)
        {
            CloseTopMenu();
        }
    }
}