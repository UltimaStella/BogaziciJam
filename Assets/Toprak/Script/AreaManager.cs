using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tolga.Scripts.Managers.DisplayMusicInGame;

struct Score
{
    public int AreaID;
    public int EnemyCount;
    public int FinishTime;
    public int ComboCount;

    public Score(int areaID, int enemyCount, int finishTime, int comboCount)
    {
        AreaID = areaID;
        EnemyCount = enemyCount;
        FinishTime = finishTime;
        ComboCount = comboCount;
    }
}

struct FinalScore
{
    public int EnemyCount;
    public int EnemyScore;
    public int FinishTime;
    public int TimeScore;
    public int ComboCount;
    public int ComboScore;
    public float AverageScore;
}

public class AreaManager : MonoBehaviour
{
    [SerializeField] int PerEnemyKillScore = 5;
    [SerializeField] int PerCompleteTimeScore = 1;
    [SerializeField] int PerComboCountScore = 10;

    public static AreaManager Instance { get; private set; }

    [SerializeField] Area[] AllAreas;
    readonly Queue<Area> VisibleAreas = new Queue<Area>(); // prev 0, current 1, next 2
    Area CurrentArea;

    List<Score> ScoreList = new List<Score>();

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
        // music
        AddThemePitchSound("themeMusic");
        PlayFinishedRoomSound();

        // prepare score
        Score newScore = new Score(
                CurrentArea.ID,
                CurrentArea.KilledEnemyCount,
                CurrentArea.AreaTime - CurrentArea.RemainTime,
                0 // Player.Instance.GetComboScore()
            );
        ScoreList.Add(newScore);

        Debug.Log("Enemy Count: " + CurrentArea.KilledEnemyCount + "\nComplete Time: " + (CurrentArea.AreaTime - CurrentArea.RemainTime));
        // change the area
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

    public bool IsInFinalArea() => CurrentArea.ID == AllAreas.Length - 1;

    public void GetFinalScores()
    {
        FinalScore finalScore = new FinalScore();
        
        foreach (Score score in ScoreList)
        {
            finalScore.ComboCount += score.ComboCount;
            finalScore.ComboScore += score.ComboCount * PerComboCountScore;

            finalScore.EnemyCount += score.EnemyCount;
            finalScore.EnemyScore += score.EnemyCount * PerEnemyKillScore;

            finalScore.FinishTime += score.FinishTime;
            finalScore.TimeScore  += score.FinishTime * PerCompleteTimeScore;
        }

        finalScore.AverageScore = (finalScore.ComboScore + finalScore.EnemyScore + finalScore.TimeScore) / 3;
    }
}
