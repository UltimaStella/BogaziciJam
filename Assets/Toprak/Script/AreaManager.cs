using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance { get; private set; }

    [SerializeField] Area[] AllAreas;
    readonly Queue<Area> VisibleAreas = new Queue<Area>(); // prev 0, current 1, next 2
    Area CurrentArea;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        EnqueueToVisiblesAreas(AllAreas[0]);
        EnqueueToVisiblesAreas(AllAreas[1]);
        SetCurrentArea(AllAreas[0]);
    }

    void EnqueueToVisiblesAreas(Area area)
    {
        area.MakeVisible();
        VisibleAreas.Enqueue(area);
    }

    void DequeueFromVisibleAreas()
    {
        Area removedArea = VisibleAreas.Dequeue();
        removedArea.DeleteArea();
    }

    void SetCurrentArea(Area area)
    {
        if (CurrentArea != null) CurrentArea.DeactivateArea();
        CurrentArea = area;
        CurrentArea.ActivateArea();
    }

    public void GoToNextArea()
    {
        if (CurrentArea.ID == AllAreas.Length - 1) return;

        int NewAreaID = CurrentArea.ID + 2;

        if (VisibleAreas.Count == 2)
        {
            Area NewArea = AllAreas[NewAreaID];
            EnqueueToVisiblesAreas(NewArea);
        }
        else
        {
            if (NewAreaID < AllAreas.Length)
            {
                Area NewArea = AllAreas[NewAreaID];
                EnqueueToVisiblesAreas(NewArea);
            }
            DequeueFromVisibleAreas();
        }
        SetCurrentArea(VisibleAreas.ToArray()[1]);
    }
}
