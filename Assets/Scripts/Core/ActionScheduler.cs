using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction currentAction;

        private void Start()
        {

        }

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
                print("Cancelling previous action");
            }

            currentAction = action;

        }

        public void CancelCurrentAction()
        {
            currentAction.Cancel();
        }
        
    }
}
