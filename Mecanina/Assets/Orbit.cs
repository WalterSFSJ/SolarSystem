using System;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private float mass;

    [SerializeField] private float linearSpeed; //AU_year


    private Vector3 V0;
    private Vector3 S0;
    private Vector3 S_i;

    private Vector3 acceleration;


    bool firstTime = true;
    private float G;
    private float sunMass = 1.0f;
    private float linearAcceleration;

    private void Start()
    {
        V0 = new Vector3(0.0f, 0.0f, linearSpeed);
        

        S0 = transform.position;

        G = 4 * Mathf.PI * Mathf.PI;
    }

    public void MoveVerlet(float dt) {


        SetAcceleration();


        Vector3 previousPosition = transform.position;

        if (firstTime)
        {
            transform.position = S0 + V0 * dt + acceleration / 2 * dt * dt;
            
            firstTime = false;
        }
        else { 
            
            transform.position = 2 * transform.position - S_i + acceleration * dt * dt;

        }

        S_i = previousPosition;
    }

    void SetAcceleration() {
        float distanceToSun = Vector3.Distance(Sun.instance.position, transform.position);
        linearAcceleration = ((G * sunMass) / (distanceToSun * distanceToSun));

        Vector3 directionToSun = (Sun.instance.position - transform.position).normalized;
        acceleration = directionToSun * linearAcceleration;
    }
    
}
