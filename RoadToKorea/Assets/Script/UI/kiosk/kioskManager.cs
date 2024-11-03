using System.Collections.Generic;
using UnityEngine;

public class kioskManager : Singleton<kioskManager>
{
    public List<string> station;
    public int selectedStationIndex;
    public int previousPanel;
    public bool isElder=false;
}
