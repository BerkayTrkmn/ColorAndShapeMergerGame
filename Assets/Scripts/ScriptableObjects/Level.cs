using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Level",menuName ="Data/Level",order =0)]
public class Level :ScriptableObject
{
    public int ID;
    public int Width =3;
    public int Height=4;

}
