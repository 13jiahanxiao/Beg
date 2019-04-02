using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knapasck : Inventory{
    private static Knapasck _instance;
    public static Knapasck Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Knapasck").GetComponent<Knapasck>();
            }
            return _instance;
        }
    }
}
