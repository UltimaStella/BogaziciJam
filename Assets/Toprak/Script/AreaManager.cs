using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct AreaContainer
{
    public Dictionary<int, Area> AllAreas;
    public Queue<Area> ActiveAreas; // prev 0, current 1, next 2
    Area CurrentArea;

    public AreaContainer(Area[] allAreas)
    {
        AllAreas = new Dictionary<int, Area>();
        for (int i = 0; i < allAreas.Length; ++i) AllAreas[allAreas[i].ID] = allAreas[i];

        ActiveAreas = new Queue<Area>();
        ActiveAreas.Enqueue(AllAreas[0]);
        ActiveAreas.Enqueue(AllAreas[1]);

        CurrentArea = AllAreas[0];
    }

    public void GoToNextArea()
    {
        if (CurrentArea.ID == AllAreas.Count - 1) return;

        int NewAreaID = CurrentArea.ID + 2;

        if (ActiveAreas.Count == 2)
        {
            Area NewArea = AllAreas[NewAreaID];
            NewArea.MakeVisible();
            ActiveAreas.Enqueue(NewArea);
        }
        else
        {
            if (NewAreaID < AllAreas.Count)
            {
                Area NewArea = AllAreas[NewAreaID];
                NewArea.MakeVisible();
                ActiveAreas.Enqueue(NewArea);
            }

            Area removedArea = ActiveAreas.Dequeue();
            removedArea.DeactivateArea();
        }
        CurrentArea = ActiveAreas.ToArray()[1];
    }
}

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance { get; private set; }

    AreaContainer AreaContainer;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        AreaContainer = new AreaContainer(FindObjectsOfType<Area>());
    }
}
