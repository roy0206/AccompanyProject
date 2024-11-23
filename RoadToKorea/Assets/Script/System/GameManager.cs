using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using StartScene;
using System;
using InGame;
using System.Collections.Generic;

[Serializable]
public class Setting
{
    [SerializeField] LanguageState language;
    public LanguageState Language { get => language; set { language = value; Translator.Instance.OnLanguageChanged(); } }
}


[Serializable]
public class PlayerData
{
    public int progress;
    public bool hard;
    
    public PlayerData()
    {
        progress = 0;
        hard = false;
    }
}
public class StageParameter
{
    Dictionary<string, object> parameters = new();
    public void AddParapeter(string str, object value)
    {
        parameters.Add(str, value);
    }

    public void AlterParameter(string str, object value)
    {
        try
        {
            parameters[str] = value;
        }
        catch
        {
            Debug.LogError("WrongParameterAlterRequest");
        }
    }

    public object GetParameter(string str)
    {
        return parameters[str];
    }
}

public class Stage1 : StageParameter
{
    public Stage1()
    {
        AddParapeter("IsTicketBought", false);
    }
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Setting setting;
    public Setting Settings => setting;

    PlayerData playerData = new();
    public PlayerData PlayerData => playerData;

    TopLayer<GameManager> stateMachine;
    public TopLayer<GameManager> StateMachine => stateMachine;
    public Dictionary<string, StageParameter> stageData;

    [SerializeField] string FSMPath;

    #region
    internal string currentScene;
    #endregion

    public Player player;

    new private void Awake()
    {
        base.Awake();
        stageData = new();
        stageData.TryAdd("Stage1", new Stage1());
        stateMachine = new GameManagerTopLayer(this);
/*        stateMachine.onFSMChange += () => { FSMPath = stateMachine.GetCurrentFSM(); }; //FSM ��� ǥ�� ������Ʈ*/
        stateMachine.OnStateEnter(); //�⺻ State ������ ���ֱ� ������ ������ ���ÿ� �ߵ� �ʿ�
    }
    private void Update()
    {
        stateMachine.OnStateUpdate();
    }
    public void PlayLevel(string sceneName)
    {
        currentScene = sceneName;
        stateMachine.ChangeState("InGameLayer");
    }

    public void OnLanguageChanged(int index)
    {
        Debug.Log(index);
        setting.Language = (LanguageState)index;
    }
}



internal class GameManagerTopLayer : TopLayer<GameManager>
{
    public GameManagerTopLayer(GameManager origin) : base(origin)
    {
        defaultState = new StartSceneLayer(origin, this);
        AddState("StartSceneLayer", defaultState);
        AddState("OutGameState", new OutGameState(origin, this));
        AddState("InGameLayer", new InGameLayer(origin, this));

    }
}
namespace StartScene
{
    internal class StartSceneLayer : Layer<GameManager>
    {
        public StartSceneLayer(GameManager origin, Layer<GameManager> parent) : base(origin, parent)
        {
            defaultState = new StartSceneWaitForTouch(origin, this);
            AddState("StartWaitForTouch", defaultState);
            AddState("StartLoading", new StartSceneLoading(origin, this));
        }
    }
    internal class StartSceneWaitForTouch : State<GameManager>
    {
        public StartSceneWaitForTouch(GameManager origin, Layer<GameManager> parent) : base(origin, parent) { }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            if (Input.touchCount > 0)
                parentLayer.ChangeState("StartLoading");
        }
    }
    internal class StartSceneLoading : State<GameManager>
    {
        float currentLoadingTime, minimumLoadingTime = 1;
        AsyncOperation asyncOperation;
        public StartSceneLoading(GameManager origin, Layer<GameManager> parent) : base(origin, parent) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            asyncOperation = SceneManager.LoadSceneAsync("OutGame");
            asyncOperation.allowSceneActivation = false;
            GameObject.Find("Canvas").transform.Find("Loading").gameObject.SetActive(true);
        }
        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            currentLoadingTime += Time.deltaTime; //�ð��� ������
            Debug.Log(asyncOperation.progress); //�ε��� �󸶳� �Ϸ�Ǿ����� 0~1�� ������ ������

            if (currentLoadingTime > minimumLoadingTime)
            {
                parentLayer.parentLayer.ChangeState("OutGameState");
            }
        }
        public override void OnStateExit()
        {
            asyncOperation.allowSceneActivation = true;
        }
    }
}

namespace InGame
{
    internal class InGameLayer : Layer<GameManager>
    {
        public InGameLayer(GameManager origin, Layer<GameManager> parent) : base(origin, parent)
        {
            defaultState = new InGameLoadingState(origin, this);
            AddState("InGameLoading", defaultState);
            AddState("InGameDefault", new InGameDefault(origin, this));
        }

        
    }
    internal class InGameLoadingState : State<GameManager>
    {
        float currentLoadingTime = 0, minimumLoadingTime = 1;
        AsyncOperation asyncOperation;
        public InGameLoadingState(GameManager origin, Layer<GameManager> parent) : base(origin, parent) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            asyncOperation = SceneManager.LoadSceneAsync(origin.currentScene);
            asyncOperation.allowSceneActivation = false;

            PersistedCanvasManager.Instance.FadeIn(0.5f);
        }
        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            currentLoadingTime += Time.deltaTime; //�ð��� ������
            Debug.Log(asyncOperation.progress); //�ε��� �󸶳� �Ϸ�Ǿ����� 0~1�� ������ ������

            if (currentLoadingTime > minimumLoadingTime)
            {
                parentLayer.ChangeState("InGameDefault");
            }
        }
        public override void OnStateExit()
        {
            asyncOperation.allowSceneActivation = true;
            PersistedCanvasManager.Instance.FadeOut(0.5f);
        }
    }
    internal class InGameDefault : State<GameManager>
    {
        public InGameDefault(GameManager origin, Layer<GameManager> parent) : base(origin, parent)
        {

        }
        public override void OnStateLateEnter()
        {
            base.OnStateLateEnter();
            origin.player = GameObject.Find("Player").GetComponent<Player>();
        }
        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
        }
    }

}
internal class OutGameState : State<GameManager>
{
    public OutGameState(GameManager origin, Layer<GameManager> parent) : base(origin, parent)
    {

    }
}






/*internal class SubStateMachine_SecondState : State<GameManager>
{
    public SubStateMachine_SecondState(GameManager origin, Layer<GameManager> parent) : base(origin, parent)
    {

    }
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();
        if (Input.GetKeyDown(KeyCode.F)) parentLayer.ChangeState("Default");
    }
}
internal class SubStateMachineThatDoesNotUseDefaultState : Layer<GameManager>
{
    public SubStateMachineThatDoesNotUseDefaultState(GameManager origin, Layer<GameManager> parent) : base(origin, parent)
    {
        AddState("State1", new State1(origin, this));
        AddState("State2", new State2(origin, this));
    }
    public override void OnStateEnter()
    {
        //base.OnStateEnter(); defaultState�� ����� �Ÿ� �ּ� ����
        if (Input.GetKey(KeyCode.R)) //R�� ���� ä�� ������Ʈ ���� �� State1, �ƴϸ� State2
        {
            currentState = states["State1"];
        }
        else
        {
            currentState = states["State2"];
        }
        currentState.OnStateEnter();
    }
}
internal class State1 : State<GameManager>
{
    public State1(GameManager origin, Layer<GameManager> parent) : base(origin, parent)
    {

    }
}
internal class State2 : State<GameManager>
{
    public State2(GameManager origin, Layer<GameManager> parent) : base(origin, parent)
    {

    }
}
*/