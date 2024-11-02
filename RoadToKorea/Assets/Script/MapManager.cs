using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[Serializable]
public class RootInfo
{
    public Destination currentDestination;
    public float moveTime;
}

public class MapManager : MonoBehaviour
{
    [SerializeField] List<RootInfo> rootInfo = new();
    public List<RootInfo> RootInfo => rootInfo;

    public int currentRootIndex;
    public bool iscameraMoving = false;
    public Translation title, contents;

    Vector2 dragVector;

    private void Start()
    {
        currentRootIndex = GameManager.Instance.PlayerData.progress;
        Camera.main.transform.position = rootInfo[currentRootIndex].currentDestination.transform.position + new Vector3(0, 0, -1);
        SelectDestination(rootInfo[currentRootIndex].currentDestination, true);
        UpdateMapDescription(rootInfo[currentRootIndex].currentDestination.stage);
    }
    public void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                dragVector = Vector2.zero;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                dragVector += Input.GetTouch(0).deltaPosition;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended  && dragVector.magnitude > 200)
            {
                MoveMap(0);
            }
        }
    }

    public void OnRightMoveTouched() { MoveMap(1); }
    public void OnLeftMoveTouched() { MoveMap(-1); }

    public void OnPlayButtonClicked()
    {
        GameManager.Instance.PlayLevel(RootInfo[currentRootIndex].currentDestination.stage.sceneName);
    }

    public void InitCamera()
    {
        Camera.main.transform.position = RootInfo[currentRootIndex].currentDestination.transform.position + new Vector3(0,0,-1);
    }
    

    private void MoveMap(int dir)
    {
        if (iscameraMoving) return;
        if((dragVector.x < 0 || dir > 0) && currentRootIndex < rootInfo.Count -1)
        {
            currentRootIndex++;
            StopAllCoroutines();
            StartCoroutine(CameraMove(rootInfo[currentRootIndex -1].currentDestination,
                rootInfo[currentRootIndex].currentDestination, rootInfo[currentRootIndex -1].moveTime));
        }
        else if((dragVector.x > 0 || dir < 0) && currentRootIndex > 0)
        {
            currentRootIndex--;
            StopAllCoroutines();
            StartCoroutine(CameraMove(rootInfo[currentRootIndex + 1].currentDestination,
                rootInfo[currentRootIndex].currentDestination, rootInfo[currentRootIndex].moveTime));
            
        }
    }

    void SelectDestination(Destination des, bool type)
    {
        des.selected.SetActive(type);
        des.idle.SetActive(!type);
    }

    void UpdateMapDescription(Stage stage)
    {
        title.ChangeText(stage.titleId);
        contents.ChangeText(stage.contentsId);
    }

    IEnumerator CameraMove(Destination from, Destination to, float time)
    {
        float curTime = 0;
        iscameraMoving = true;

        SelectDestination(from, false);
        while (curTime < time)
        {
            curTime += Time.deltaTime;
            Camera.main.transform.position = (Vector3)Vector2.Lerp(from.transform.position, to.transform.position, curTime / time) + new Vector3(0,0, -1);
            yield return null;
        }
        iscameraMoving = false;
        SelectDestination(to, true);
        UpdateMapDescription(to.stage);
    }
}
