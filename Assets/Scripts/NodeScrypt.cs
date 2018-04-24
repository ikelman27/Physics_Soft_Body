using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScrypt : MonoBehaviour
{


    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 force;
    public float mass;
    public bool isAnchor;
    public List<GameObject> connectedNodes;

    public List<float> springRest;

    public Material lineMat;

    // Use this for initialization
    void Start()
    {
        position = gameObject.transform.position;
        springRest = new List<float>();

        for (int i = 0; i < connectedNodes.Count; i++)
        {
            springRest.Add(Vector3.Magnitude(gameObject.transform.position - connectedNodes[i].transform.position)/1.5f);
            Debug.Log("spring Rest  " + springRest[i] + "  " + name);
        }
       if(mass == 0)
        {
            mass = 1.0f;
        }
        
        
    }

    // Update is called once per frame
    private void Update()
    {
        position = gameObject.transform.position;
        if (!isAnchor)
        {
            for (int i = 0; i < connectedNodes.Count; i++)
            {
                Vector3 deltaV = this.velocity - connectedNodes[i].GetComponent<NodeScrypt>().velocity;
                Vector3 springDir = position - connectedNodes[i].GetComponent<NodeScrypt>().position;
                float springCurrent = Vector3.Magnitude(position - connectedNodes[i].GetComponent<NodeScrypt>().position);
                Vector3 springForce = -8f * ((springCurrent - springRest[i])) * springDir;

                springForce += (float)-.3f * Vector3.Dot(deltaV, springDir) * springDir;

                applyForce(springForce);

                if (springCurrent > springRest[i])
                {
                    Debug.DrawLine(position, connectedNodes[i].GetComponent<NodeScrypt>().position, Color.red);
                }
                else
                {
                    Debug.DrawLine(position, connectedNodes[i].GetComponent<NodeScrypt>().position, Color.green);
                }
            }


            //if the obj is an anchor it cant move

            //basic Integration and position
            acceleration = force / mass;

            velocity += acceleration * Time.deltaTime;
            velocity *= .99f;
            position += velocity * Time.deltaTime;

            gameObject.transform.position = position;




            force = Vector3.zero;
            //force = new Vector3(0, -9.8f * mass/2, 0);

        }
        else
        {
            if (Time.time > 3.14159)
            {
                //position.y = Time.deltaTime + Mathf.Sin(Time.time);
                //WDebug.Log(Mathf.Cos(Time.time*2*Mathf.PI*180));
                //gameObject.transform.position = position;
            }
        }
    }


    public void applyForce(Vector3 newForce)
    {
        force += newForce;
    }
}
