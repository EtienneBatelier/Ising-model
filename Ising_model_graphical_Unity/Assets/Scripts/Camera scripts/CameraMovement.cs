using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour {
 
    /*
    Inspired by Windexglow's 11-13-10 version they made freely available on GitHub.  
    WASD control basic movement, space and X control up and down movement, shift sprints.
    */
     
    float baseSpeed = 10.0f;
    float shiftAdd = 25.0f; 
    float maxSpeed = 100.0f; 
    float camSens = 0.15f;          //Mouse sensitivity
    private Vector3 lastMouse;     
    private float totalRun = 1.0f;

     
    void Update() 
    {
        //Mouse commands
        lastMouse = Input.mousePosition - lastMouse ;
        if (Input.GetKey (KeyCode.Mouse1))
        {
            lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0 );
            lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
            transform.eulerAngles = lastMouse;
        } 
        lastMouse =  Input.mousePosition;
    
        
        //Keyboard commands
        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0)
        {
            if (Input.GetKey (KeyCode.LeftShift))
            {
                totalRun += Time.deltaTime;
                p *= totalRun * shiftAdd;
                p.x = Mathf.Clamp(p.x, -maxSpeed, maxSpeed);
                p.y = Mathf.Clamp(p.y, -maxSpeed, maxSpeed);
                p.z = Mathf.Clamp(p.z, -maxSpeed, maxSpeed);
            } 
            else 
            {
                totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                p = p * baseSpeed;
            }
         
            p *= Time.deltaTime;
            transform.Translate(p);
        }
    }
     
    private Vector3 GetBaseInput() 
    {
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey (KeyCode.W)) {p_Velocity.z += 1;}
        if (Input.GetKey (KeyCode.S)) {p_Velocity.z -= 1;}
        if (Input.GetKey (KeyCode.A)) {p_Velocity.x -= 1;}
        if (Input.GetKey (KeyCode.D)) {p_Velocity.x += 1;}
        if (Input.GetKey (KeyCode.X)) {p_Velocity.y -= 1;}
        if (Input.GetKey (KeyCode.Space)) {p_Velocity.y += 1;}
        return p_Velocity;
    }
}