using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace HelloScripts
{
    [ExecuteAlways]
    public class CameraFollow : MonoBehaviour
    {
        public GameObject objectToFollow;

        public Vector3 offset;
        [HideInInspector] public Vector3 startingOffset;
        public Vector3 rotation;
        [HideInInspector] public Vector3 startRotation;
        private bool isChanging = false;
        

        public bool isSmoothCameraMovement = false;
        private Vector3 current_vel;
        public float moveSpeed = 2;

       
        void Start()
        {
            
            startingOffset = offset;
            startRotation = rotation;
        }


        void LateUpdate()
        {

            if (!isSmoothCameraMovement)
            {
                if (!isChanging)
                    if (objectToFollow != null)
                    {
                        transform.position = objectToFollow.transform.position + offset;
                        transform.localEulerAngles = rotation;
                    }
            }
            else
            {
                if (!isChanging)
                    if (objectToFollow != null)
                    {
                        Vector3 target_pos = objectToFollow.transform.position + offset;
                         Vector3 temp =  Vector3.SmoothDamp(transform.position, target_pos, ref current_vel, 1f / moveSpeed);
                        transform.position = new Vector3(temp.x, objectToFollow.transform.position.y + offset.y, objectToFollow.transform.position.z + offset.z);
                        transform.localEulerAngles = rotation;
                    }
            }

        }


        public void ChangeFollowingObject(GameObject go, float changingTime =1f)
        {
            isChanging = true;
            objectToFollow = go;
            gameObject.transform.DOMove(objectToFollow.transform.position + offset, changingTime).OnComplete(() => { isChanging = false; });
            gameObject.transform.DORotate(rotation, changingTime);

        }
        public void MoveCameraWithTime(Vector3 newOffset, Vector3 newRotation, float moveTime, Action endFunction =null)
        {
            offset = newOffset;
            rotation = newRotation;
            isChanging = true;
            gameObject.transform.DORotate(newRotation, moveTime);
            gameObject.transform.DOMove(objectToFollow.transform.position + newOffset, moveTime).OnComplete(() => {
                if(endFunction != null)endFunction.Invoke(); isChanging = false; });


        }
        public void ResetCamera(float resetTime)
        {
            offset = startingOffset;
            isChanging = true;
            gameObject.transform.DORotate(startRotation, resetTime);
            gameObject.transform.DOMove(objectToFollow.transform.position + offset, resetTime).OnComplete(() => { isChanging = false; });
        }
        public void ChangeCameraOffset(Vector3 _offset, Vector3 _rotation, float changeTime)
        {
            offset = _offset;
            rotation = _rotation;
            isChanging = true;
            gameObject.transform.DORotate(rotation, changeTime);
            gameObject.transform.DOMove(objectToFollow.transform.position + offset, changeTime).OnComplete(() => { isChanging = false; });
        }
        public void ChangeCameraOffset(Vector3 _offset, Vector3 _rotation, float changeTime, Action endOfMovement = null)
        {
            offset = _offset;
            rotation = _rotation;
            isChanging = true;
            gameObject.transform.DORotate(rotation, changeTime);
            gameObject.transform.DOMove(objectToFollow.transform.position + offset, changeTime).OnComplete(() => {
                if(endOfMovement != null)endOfMovement.Invoke(); isChanging = false; });
        }
    }
}