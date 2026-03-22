using System;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private float radius; //AU
    [SerializeField] private float linearSpeed; //AU_year

    Vector3 V0;
    Vector3 S0;
    Vector3 S_i;

    Vector3 acceleration;

    float time;

    bool firstTime = true;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x * 2, 0.0f, 0.0f);

        radius = transform.position.x;

        acceleration = new Vector3(0.0f, 0.0f, linearSpeed);
        V0 = acceleration;

        S0 = transform.position;
    }

    public void MoveAnalytical(float dt)
    {
        
        float currentAngle = GetRadiansFromSun();
        float newAngle = currentAngle + (linearSpeed * dt);
        
       
        float newX = radius * Mathf.Cos(newAngle) ;
        float newZ = radius * Mathf.Sin(newAngle);

        transform.position = new Vector3(newX, transform.position.y, newZ);
    }



    public void MoveVerlet(float dt) {

        Vector3 directionToSun = (Sun.instance.position - transform.position).normalized;
        acceleration = directionToSun * (linearSpeed * linearSpeed / radius);

        if (firstTime)
        {
            transform.position = S0 + V0 * dt + acceleration / 2 * dt * dt;
            
            S_i = S0;

            firstTime = false;
        }
        else { 
            
            Vector3 previousPosition = transform.position;
            
            transform.position = 2 * transform.position - S_i + acceleration * dt * dt;

            S_i = previousPosition;
        }
        
    }

    float GetRadiansFromSun() {

        Vector3 distance = transform.position - Sun.instance.position;

        return Mathf.Atan2(distance.z, distance.x);
    }
    
}
