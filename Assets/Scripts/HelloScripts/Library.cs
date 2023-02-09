using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HelloScripts
{
    public static class Library
    {
        /// <summary>
        /// Destroys wanted component inside children
        /// </summary>
        /// <typeparam name="T">Destroyed Component</typeparam>
        /// <param name="parentTrans"></param>
        public static void DestroyAllChildWithT<T>(this Transform parentTrans) where T : Component
        {
            T[] components = parentTrans.GetComponentsInChildren<T>();

            foreach (T component in components)
            {
                Object.Destroy(component.gameObject);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentTrans"></param>
        /// <param name="activity"></param>
        public static void ChangeActivityAllListOfGameObjects(this List<GameObject> parentTrans, bool activity)
        {
            foreach (GameObject go in parentTrans)
            {
                go.SetActive(activity);
            }
        }

        ///For ragdoll character explosion
        #region Character Explosion

        /// <summary>
        /// Close/Open all child rigidbody's and adds force / mostly for ragdoll
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="activity">is kinematic activity</param>
        /// <param name="xforce"></param>
        /// <param name="yforce"></param>
        /// <param name="zforce"></param>
        /// <param name="isForced">is force applies?</param>
        public static void ChangeActiveRBAllChildren(this Transform parent, bool activity, float xforce, float yforce, float zforce, bool isForced = true)
        {
            Rigidbody[] rbs = parent.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody rb in rbs)
            {
                rb.isKinematic = activity;
                if (isForced) rb.AddForce(xforce, yforce, zforce);
            }
        }
        /// <summary>
        /// Close/Open all child colliders / mostly for ragdoll
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="activity"></param>
        public static void ChangeActivityAllColliderInChildren(this Transform parent, bool activity)
        {
            Collider[] colliders = parent.GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders)
            {
                col.enabled = activity;
            }
        }

        /// <summary>
        /// Ragdoll transform explodes
        /// </summary>
        /// <param name="explodedCharacter"></param>
        /// <param name="explosionMultiplier">Vector of explosion (direction and magnitude) </param>
        public static void CharacterExplosion(this Transform explodedCharacter, Vector3 explosionForce, bool isForced = true)
        {
            ChangeActiveRBAllChildren(explodedCharacter.transform, false, explosionForce.x, explosionForce.z, explosionForce.z, isForced);
            ChangeActivityAllColliderInChildren(explodedCharacter, true);
            //explodedCharacter.transform.GetComponent<Animator>().enabled = false;
            // explodedCharacter.transform.parent = null;
            foreach (Collider col in explodedCharacter.GetComponents<Collider>())
                col.enabled = false;

        }
        /// <summary>
        /// Direction of two transform
        /// </summary>
        /// <param name="startTrans"></param>
        /// <param name="endTrans"></param>
        /// <returns></returns>
        public static Vector3 DirectionVector(this Transform startTrans, Transform endTrans)
        {
            return endTrans.position - startTrans.position;
        }
        #endregion
        /// <summary>
        /// Direction of two vector
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public static Vector3 DirectionVector(this Vector3 startPoint, Vector3 endPoint)
        {
            return endPoint - startPoint;
        }
        /// <summary>
        /// Creates objects and sets  transform position
        /// </summary>
        /// <param name="createdGO"></param>
        /// <param name="trans"></param>
        public static GameObject CreateGameObjectandPlaceIt(this GameObject prefab, Transform trans)
        {
            GameObject go = Object.Instantiate(prefab);

            go.transform.position = trans.position;
            return go;
        }
        /// <summary>
        /// Creates objects and sets  transform position
        /// </summary>
        /// <param name="createdGO"></param>
        /// <param name="trans"></param>
        public static GameObject CreateGameObjectandPlaceIt(this GameObject prefab, Vector3 position)
        {
            GameObject go = Object.Instantiate(prefab);

            go.transform.position = position;
            return go;
        }
        /// <summary>
        /// Creates object and sets rotation and position
        /// </summary>
        /// <param name="createdGO"></param>
        /// <param name="trans"></param>
        /// <param name="rotation"></param>
        public static void CreateGameObjectandPlaceIt(this GameObject prefab, Transform trans, Vector3 rotation)
        {
            GameObject go = Object.Instantiate(prefab);

            go.transform.position = trans.position;
            go.transform.eulerAngles = rotation;
        }

        /// <summary>
        /// Resets all TRIGGERS
        /// </summary>
        /// <param name="animator"></param>
        public static void ResetAllAnimatorTriggers(this Animator animator)
        {
            foreach (var trigger in animator.parameters)
            {
                if (trigger.type == AnimatorControllerParameterType.Trigger)
                {
                    animator.ResetTrigger(trigger.name);
                }
            }
        }
        public static Dictionary<float, WaitForSeconds> waitList;
        /// <summary>
        /// Store wfs for next usage for better optimization
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static WaitForSeconds GetWait(this float time)
        {
            if (waitList == null) waitList = new Dictionary<float, WaitForSeconds>();
            if (!waitList.ContainsKey(time))
            {
                WaitForSeconds waitTime = new WaitForSeconds(time);
                waitList.Add(time, waitTime);
                return waitTime;
            }
            else
            {
                return waitList[time];
            }

        }
        /// <summary>
        /// Destroys object
        /// </summary>
        /// <param name="go"></param>
        public static void Destroy(this GameObject go)
        {
            Object.Destroy(go);
        }


        /// <summary>
        /// Object moves one point to another with a speed. This function gives
        /// how much time consumed while moving.
        /// </summary>
        /// <param name="startingTransform"></param>
        /// <param name="endTransform"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static float GetMovingTime(this Transform startingTransform, Transform endTransform, float speed)
        {
            return Vector3.Distance(startingTransform.position, endTransform.position) / speed;
        }
        /// <summary>
        /// Object moves one point to another with a speed. This function gives
        /// how much time consumed while moving.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static float GetMovingTime(this Vector3 startPoint, Vector3 endPoint, float speed)
        {
            return Vector3.Distance(startPoint, endPoint) / speed;
        }
        /// <summary>
        /// OnlyX axis changing
        /// </summary>
        /// <param name="changingVector"></param>
        /// <param name="Xinput"></param>
        /// <returns></returns>
        public static Vector3 ChangeX(this Vector3 changingVector, float Xinput)
        {
            return new Vector3(Xinput, changingVector.y, changingVector.z);
        }
        /// <summary>
        /// OnlyY axis changing
        /// </summary>
        /// <param name="changingVector"></param>
        /// <returns></returns>
        public static Vector3 ChangeY(this Vector3 changingVector, float Yinput)
        {
            return new Vector3(changingVector.x, Yinput, changingVector.z);
        }
        /// <summary>
        /// OnlyZ axis changing
        /// </summary>
        /// <param name="changingVector"></param>
        /// <returns></returns>
        public static Vector3 ChangeZ(this Vector3 changingVector, float Zinput)
        {
            return new Vector3(changingVector.x, changingVector.y, Zinput);
        }
        /// <summary>
        /// OnlyXY axis changing
        /// </summary>
        /// <param name="changingVector"></param>
        /// <returns></returns>
        public static Vector3 ChangeXY(this Vector3 changingVector, float Xinput, float Yinput)
        {
            return new Vector3(Xinput, Yinput, changingVector.z);
        }
        /// <summary>
        /// OnlyXZ axis changing
        /// </summary>
        /// <param name="changingVector"></param>
        /// <returns></returns>
        public static Vector3 ChangeXZ(this Vector3 changingVector, float Xinput, float Zinput)
        {
            return new Vector3(Xinput, changingVector.y, Zinput);
        }
        /// <summary>
        /// OnlyYZ axis changing
        /// </summary>
        /// <param name="changingVector"></param>
        /// <param name="Yinput"></param>
        /// <param name="Zinput"></param>
        /// <returns></returns>
        public static Vector3 ChangeYZ(this Vector3 changingVector, float Yinput, float Zinput)
        {
            return new Vector3(changingVector.x, Yinput, Zinput);
        }
        /// <summary>
        /// Unity % symbol not correct for minus number this func is correction of this 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="modNumber"></param>
        /// <returns></returns>
        public static int Mod(this int x, int modNumber)
        {
            int r = x % modNumber;
            return r < 0 ? r + modNumber : r;
        }
        /// <summary>
        /// Unity % symbol not correct for minus number this func is correction of this 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="modNumber"></param>
        /// <returns></returns>
        public static float Mod(this float x, float modNumber)
        {
            float r = x % modNumber;
            return r < 0 ? r + modNumber : r;
        }

        /// <summary>
        /// Raycast needs collider to hit
        /// </summary>
        /// <param name="physics"></param>
        /// <param name="mousePosition">Mouse/Touch position</param>
        /// <returns></returns>
        public static RaycastHit2D RaycastFromMouseToScreen(this RaycastHit2D physics, Vector2 mousePosition)
        {
            physics = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            return physics;
        }

        public static T CopyClassValues<T>(this T sourceComp, T targetComp) where T : Component
        {
            FieldInfo[] sourceFields = sourceComp.GetType().GetFields(BindingFlags.Public |
                                                             BindingFlags.NonPublic |
                                                             BindingFlags.Instance);
            int i = 0;
            for (i = 0; i < sourceFields.Length; i++)
            {
                var value = sourceFields[i].GetValue(sourceComp);
                sourceFields[i].SetValue(targetComp, value);
            }
            return targetComp;
        }
        /// <summary>
        /// Compy component to other object component
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comp"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static T GetCopyOf<T>(this Component comp, T other) where T : Component
        {
            Type type = comp.GetType();
            if (type != other.GetType()) return null; // type mis-match
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
            PropertyInfo[] pinfos = type.GetProperties(flags);
            foreach (var pinfo in pinfos)
            {
                if (pinfo.CanWrite)
                {
                    try
                    {
                        pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                    }
                    catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
                }
            }
            FieldInfo[] finfos = type.GetFields(flags);
            foreach (var finfo in finfos)
            {
                finfo.SetValue(comp, finfo.GetValue(other));
            }
            return comp as T;
        }
        /// <summary>
        /// Create and copy of component
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <param name="toAdd"></param>
        /// <returns></returns>
        //public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
        //{
        //    return go.AddComponent<T>().GetCopyOf(toAdd) as T;
        //}
    }



}