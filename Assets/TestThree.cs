using System.Collections.Generic;
using UnityEngine;


public class TestThree : MonoBehaviour
{
    public int MyInt;
    public int MyInt2 => 4;
    public int MyInt3 { get; set; }

    public List<GameObject> obs1;
    public List<GameObject> obs2;
    public List<GameObject> obs3 { get; set; }
    public PriorityQueue<GameObject, int> ppp { get; set; }
}
