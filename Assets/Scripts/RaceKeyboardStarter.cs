using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceKeyboardStarter : MonoBehaviour
{
   [SerializeField] private RaceStateTracker m_raceStateTracker;

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Return) == true)
      {
         m_raceStateTracker.StartRaceTimer();
      }
   }
}
