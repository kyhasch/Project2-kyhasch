using UnityEngine;

public class Fakegoal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         this.transform.localRotation = Quaternion.Euler(0, -1, 0) * this.transform.localRotation;
    }
}
