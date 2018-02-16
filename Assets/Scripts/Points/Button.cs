using System.Collections.Generic;
using UnityEngine;

namespace Points
{
    public class Button : MonoBehaviour
    {
        public string WhatToDo;

        public void OnMouseDown()
        {
            Invoke(WhatToDo, 0);
        }

        private void DoPlay()
        {
            SceneManager.Play();
        }

        private void DoReset()
        {
            SceneManager.ResetPoints();
        }
    }
}