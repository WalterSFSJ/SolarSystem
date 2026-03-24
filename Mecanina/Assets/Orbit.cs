using System;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private float mass;

    [SerializeField] private float radius; //AU
    [SerializeField] private float linearSpeed; //AU_year


    private Vector3 V0;
    private Vector3 S0;
    private Vector3 S_i;

    private Vector3 acceleration;


    bool firstTime = true;
    private float G = 6.6742f;
    private float sunMass = 1.0f;
    private float linearAcceleration;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, 0.0f, 0.0f);

        radius = transform.position.x;

        V0 = new Vector3(0.0f, 0.0f, linearSpeed);
        

        S0 = transform.position;

                  
    }

    public void MoveVerlet(float dt) {
        float distanceToSun = Vector3.Distance(Sun.instance.position, transform.position);
        linearAcceleration = ((G * sunMass) / (distanceToSun * distanceToSun));

        Vector3 directionToSun = (Sun.instance.position - transform.position).normalized;
        acceleration = directionToSun * linearAcceleration;


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
    
}
