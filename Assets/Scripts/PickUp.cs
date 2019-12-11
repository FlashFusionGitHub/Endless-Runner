using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    enum Size { small = 1, medium, large };

    [SerializeField]
    Size size { get; set; }
    [SerializeField]
    int worth { get; set; }
    [SerializeField]
    float fuel { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
