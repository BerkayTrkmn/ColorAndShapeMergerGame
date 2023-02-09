using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelloScripts
{

    //[CreateAssetMenu(fileName = "Level", menuName = "Custom/Level", order = 0)]
    public class Level : ScriptableObject
    {

        public int levelId;
        public Platform[] levelPlatforms;
        public Platform endingPlatform;



    }
    //On implementation give this script to platform object
    public class Platform : MonoBehaviour
    {
        public Transform endPoint;
    }


    public class LevelControllerTemplate : MonoBehaviour
    {
        public List<Level> levelList;

        public Transform currentEndPoint;
        public int levelNumber = 1;

        private void Start()
        {
            int currentLevel = levelNumber % levelList.Count;
            SetLevel(levelList[currentLevel]); ;
        }

        public void SetLevel(Level level)
        {
            Platform[] platforms = level.levelPlatforms;

            for (int i = 0; i < platforms.Length; i++)
            {
                GameObject currentPlatformGO = CreatePlatform(platforms[i]);
                Platform platform = currentPlatformGO.GetComponent<Platform>();
                currentEndPoint = platform.endPoint;
            }
            CreateLevelPart(level.endingPlatform.gameObject);
        }


        public GameObject CreatePlatform(Platform platform)
        {
            GameObject go = Instantiate(platform.gameObject, currentEndPoint.position, transform.rotation, transform);
            return go;
        }
        public GameObject CreateLevelPart(GameObject _go)
        {
            GameObject go = Instantiate(_go, currentEndPoint.position, transform.rotation, transform);
            return go;
        }
    }

}