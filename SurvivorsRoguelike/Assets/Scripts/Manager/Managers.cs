using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance;
    private static Managers Instance
    {
        get
        {
            Init();
            return s_instance;
        }
    }

    private static bool s_isQuitting = false;

    // Core
    private DataManager _data = new DataManager();
    private EventManager _event = new EventManager();
    private ObjectManager _object = new ObjectManager();
    private PoolManager _pool = new PoolManager();
    private ResourceManager _resource = new ResourceManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private UIManager _ui = new UIManager();

    public static DataManager Data { get { return s_isQuitting ? null : Instance._data; } }
    public static EventManager Event { get { return s_isQuitting ? null : Instance._event; } }
    public static ObjectManager Object { get { return s_isQuitting ? null : Instance._object; } }
    public static PoolManager Pool { get { return s_isQuitting ? null : Instance._pool; } }
    public static ResourceManager Resource { get { return s_isQuitting ? null : Instance._resource; } }
    public static SceneManagerEx Scene { get { return s_isQuitting ? null : Instance._scene; } }
    public static UIManager UI { get { return s_isQuitting ? null : Instance._ui; } }

    // Contents
    private GridManager _grid = new GridManager();
    private SkillManager _skill = new SkillManager();

    public static GridManager Grid { get { return s_isQuitting ? null : Instance._grid; } }
    public static SkillManager Skill { get { return s_isQuitting ? null : Instance._skill; } }

    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (s_instance == null)
        {
            GameObject gameObject = GameObject.Find("_Managers");
            if (gameObject == null)
            {
                gameObject = new GameObject { name = "_Managers" };
                gameObject.AddComponent<Managers>();
            }
            
            DontDestroyOnLoad(gameObject);
            s_instance = gameObject.GetComponent<Managers>();
        }
    }

    public static void Clear()
    {
        // Contents
        s_instance._skill.Clear();
        s_instance._grid.Clear();

        // Core
        s_instance._object.Clear();
        s_instance._pool.Clear();
        s_instance._ui.Clear();
        s_instance._event.Clear();
        s_instance._scene.Clear();
    }

    private void OnApplicationQuit()
    {
        s_instance = null;
        s_isQuitting = true;
    }
}
