using System;
using DG.Tweening;
using UnityEngine;

namespace TF_Shooting.CodeBaseTF
{
    public class Cross : MonoBehaviour
    {
        public Plate Target { get; set; }

        private void Update()
        {
            if (Target == null)
                return;

            transform.DOMoveY(Target.transform.position.y, 0.1f);


#if UNITY_EDITOR
            if (!Input.GetMouseButtonDown(0))
                return;

            CheckGoal();
#elif UNITY_ANDROID
            if (Input.touchCount <= 0)
                return;

            Touch touch = Input.GetTouch(0);
            
             if (touch.phase == TouchPhase.Began)
                 CheckGoal();
#endif
        }

        private void CheckGoal()
        {
            if (Math.Abs(Target.transform.position.x - transform.position.x) < 2f)
                Target.Reset();
        }
    }
}