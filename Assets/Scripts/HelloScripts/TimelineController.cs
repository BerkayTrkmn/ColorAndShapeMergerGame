using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;
using System;

public class TimelineController : MonoBehaviour
{
    public static TimelineController instance;
    public PlayableDirector director;
    TimelineAsset timelineAsset;
    List<TrackAsset> startStateTracks;
   [HideInInspector] public List<bool> rootStartStates;

    void Awake()
    {
        #region Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        #endregion
        timelineAsset = (TimelineAsset)director.playableAsset;
        rootStartStates = GetStartStatesAllRoots();
       
    }

    private void OnEnable()
    {
      SetTracksAtStartOfLevel();
    }


    public List<bool> GetStartStatesAllRoots()
    {
        startStateTracks = timelineAsset.GetRootTracks().ToList();
        List<bool> allStartStates = new List<bool>();
        foreach (TrackAsset track in startStateTracks)
        {
            allStartStates.Add(track.muted);
        }

        return allStartStates;
    }

    public void Play()
    {
        director.Play();
    }

    public void Pause()
    {
        director.Pause();
    }

    public void GetGroupTrack(int index)
    {
        Debug.Log(timelineAsset.GetRootTrack(index).name);
    }
  //button events get only 1 and zero parameters 
    public void MuteGroupTrack(int index)
    {
        timelineAsset.GetRootTrack(index).muted = true;
        RebuildGraphWithTimer();
    }
    public void UnmuteGroupTrack(int index)
    {
        timelineAsset.GetRootTrack(index).muted = false;
        RebuildGraphWithTimer();
    }
    /// <summary>
    /// Every changes in the game needs rebuild
    /// </summary>
    public void RebuildGraphWithTimer()
    {
        double checkTime = director.time;
        director.RebuildGraph();
        director.time = checkTime;
    }

    public void ReturnStartStateAllRoots(List<bool> rootStartStates)
    {
        for (int i = 0; i < startStateTracks.Count; i++)
        {
            startStateTracks[i].muted = rootStartStates[i];
        }
        RebuildGraphWithTimer();
    }

    private void OnApplicationPause(bool pause)
    {
        ReturnStartStateAllRoots(rootStartStates);
    }
    public void CloseTimeline()
    {
        director.enabled = false;
    }
    public void SetTracksAtStartOfLevel()
    {
        UnmuteGroupTrack(1);
        MuteGroupTrack(2);
        MuteGroupTrack(3);

    }
}
