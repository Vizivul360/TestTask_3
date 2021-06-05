using UnityEngine;
using Ecs.Systems;
using Collisions;
using Models;
using Game;
using Ecs;
using UI;

public partial class GameBehaviour : MonoBehaviour, Source, OnTime
{
    public float startDelay;

    public bool _render;
    public bool render { get { return _render; } }

    private Executor drawGizmos;
    private ISystems game;

    private CameraScaler scaler;
    private Timer timer;

    void Awake()
    {
        createPools();

        form = formObj.GetComponent<IFormBind>();
        form.SetGame(this);

        var context = new Context();

        scaler = GetComponent<CameraScaler>();
        timer = GetComponent<Timer>();
        timer.enabled = false;
        
        drawGizmos = new GizmoSystem(context, this);
        game = new GameSystems(context, this);

        scaler.SetScale(map.size);
        game.Init();

        timer.Run(startDelay, this);
        enabled = false;
    }

    private void createPools()
    {
        _collisions = new CollisionPool();
        _models = new ModelPool(modelConfig);
    }

    void FixedUpdate()
    {
        game.Update();
    }

    public void Pause()
    {
        enabled = false;

        if (timer.active)
        {
            timer.enabled = false;
            OnSeconds(0);
        }
    }

    public void Resume()
    {
        if (timer.active)
        {
            timer.enabled = true;
        }
        else
        {
            enabled = true;
        }
    }

    public void Loose()
    {
        enabled = false;
        form.Loose();
    }

    public void Win()
    {
        enabled = false;
        form.Win();
    }

    public void OnSeconds(float sec)
    {
        form.SetTimer(sec);
    }

    public void TimeEnd()
    {
        enabled = true;
        OnSeconds(0);
    }
}

//Mechanics
public partial class GameBehaviour
{
    [Space]
    public MapConfig mapConfig;
    public int index;

    [Space]
    public UnitConfig unitConfig;
    
    [Space]
    public bool _rawAxis;

    private CollisionPool _collisions;

    public IMap map
    {
        get { return mapConfig[index]; }
    }

    public ICollision collision
    {
        get { return _collisions; }
    }

    public IUnits meta
    {
        get { return unitConfig; }
    }

    public float deltaTime
    {
        get { return Time.fixedDeltaTime; }
    }

    public bool rawAxis
    {
        get { return _rawAxis; }
    }
}

//Views
public partial class GameBehaviour
{
    [Space]
    public ModelConfig modelConfig;

    public GameObject formObj;

    public IFormBind form { get; private set; }

    private ModelPool _models;

    public object GetModel(string name)
    {
        return _models.Get(name);
    }
}

//Gizmos
public partial class GameBehaviour
{
    [Space]
    public bool _gizmos;

    public bool _level, _units, _projectiles;

    public bool level
    {
        get { return _level; }
    }

    public bool projectiles
    {
        get { return _projectiles; }
    }

    public bool units
    {
        get { return _units; }
    }

    void OnDrawGizmos()
    {
        if (_gizmos && drawGizmos != null) drawGizmos.Exec();
    }
}