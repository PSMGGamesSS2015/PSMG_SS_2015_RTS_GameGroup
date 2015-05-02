using UnityEngine;
using System.Collections;

public class ImpController : MonoBehavior
{

    public enum JOB
    {
        NONE,
        SPEARMAN,
        GUARDIAN,
        LADDER_CARRIER,
        BLASTER
    }

    JOB job;

    void Awake()
    {
        job = JOB.NONE;
    }

    void OnMouseDown()
    {
        Debug.Log("You clicked on an imp.");
    }

    void Update()
    {
        CheckCollisions();
        Move();
    }

    void Move()
    {
        // TODO
    }

    void CheckCollisions() 
    {
        // TODO
    }

    public void Train(JOB job)
    {
        this.job = job;
    }

    public bool HasJob() {
        return !(job == JOB.NONE);
    }

    void RemoveJob()
    {
        this.job = JOB.NONE;
    }

}