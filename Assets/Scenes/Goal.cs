using UnityEngine;
using UnityEngine.UI;
public class Goal : MonoBehaviour
{
    public Text winText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winText.enabled = false;
    }
    void OnTriggerEnter(Collider other){
          Debug.Log("Something entered the goal: " + other.gameObject.name + " | Tag: " + other.gameObject.tag);
        if(other.gameObject.tag == "Ball"){
            Debug.Log("Goal Scored!");
            winText.enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.localRotation = 
               Quaternion.Euler(0,1,0) * this.transform.localRotation;
    }
}
