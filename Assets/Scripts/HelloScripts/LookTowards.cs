using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowards : MonoBehaviour
{

   public Transform lookObject;

   
    
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(lookObject.position-transform.position ) ;
    }
}
