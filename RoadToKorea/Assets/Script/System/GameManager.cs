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
    [SerializeField]Language language;
    public Language Language { get => language; set { language = value; Translator.Instance.OnLanguageChanged(); } }
}


[Serializable]
public class PlayerData
{
    public int progress;
    public PlayerData()
    {
        progress = 0;
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


    new private void Awake()
    {
        base.Awake();
        stageData = new();
        stageData.TryAdd("Stage1", new Stage1());
        stateMachine = new GameManagerTopLayer(this);
/*        stateMachine.onFSMChange += () => { FSMPath = stateMachine.GetCurrentFSM(); }; //FSM 경로 표시 업데이트*/
        stateMachine.OnStateEnter(); //기본 State 세팅을 해주기 때문에 생성과 동시에 발동 필요

        
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
        setting.Language = (Language)index;
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
            asyncOperation = SceneManager.LoadSceneAsync("OutGame");
            asyncOperation.allowSceneActivation = false;
            GameObject.Find("Canvas").transform.Find("Loading").gameObject.SetActive(true);
        }
        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            currentLoadingTime += Time.deltaTime; //시간을 더해줌
            Debug.Log(asyncOperation.progress); //로딩이 얼마나 완료되었는지 0~1의 값으로 보여줌

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
            asyncOperation = SceneManager.LoadSceneAsync(origin.currentScene);
            asyncOperation.allowSceneActivation = false;

            PersistedCanvasManager.Instance.FadeIn(0.5f);
        }
        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            currentLoadingTime += Time.deltaTime; //시간을 더해줌
            Debug.Log(asyncOperation.progress); //로딩이 얼마나 완료되었는지 0~1의 값으로 보여줌

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
        //base.OnStateEnter(); defaultState를 사용할 거면 주석 해제
        if (Input.GetKey(KeyCode.R)) //R을 누른 채로 스테이트 진입 시 State1, 아니면 State2
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