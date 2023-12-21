using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This function is to help with randomizing wind behavior to give the curtain a more dynamic feel, as the cloth component's acceleration
//can feel rigid and too consistent
public class WindSimulator : MonoBehaviour
{
    private Cloth clothMod;
    private Vector3 TargetWindForce;
    //Finds the cloth component and allows the changing of the wind force and wind force interval in the inspector

    public Vector3 MaxWindForce;
    public float WindForceIntervals = 1;
    public Vector3 NegativeWindForce;

    void Start()
    {
        //Sets initial external acceleration to the negative wind force to blow in a specific direction
        clothMod = this.GetComponent<Cloth>();
        clothMod.externalAcceleration = NegativeWindForce;
        TargetWindForce = MaxWindForce;
        if (WindForceIntervals <= 0)
        {
            WindForceIntervals = 0.1f;
        }
    }
    void Update()
    {
        //Changes the external acceleration towards the max wind force
        clothMod.externalAcceleration = Vector3.MoveTowards(clothMod.externalAcceleration,
            TargetWindForce, WindForceIntervals * Time.deltaTime);
        //Once the external acceleration hits the maximum wind force, it will make switch to negative wind force, which changes its direction
        if (clothMod.externalAcceleration == MaxWindForce)
        {
            TargetWindForce = NegativeWindForce;
        }
        //Same as above, will change direction once negative wind force is hit 
        if (clothMod.externalAcceleration == NegativeWindForce)
        {
            TargetWindForce = MaxWindForce;
        }
    }
}   