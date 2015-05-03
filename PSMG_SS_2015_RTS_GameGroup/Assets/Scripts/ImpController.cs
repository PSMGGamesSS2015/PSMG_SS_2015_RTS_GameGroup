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

    // Events
    public delegate void ImpSelected(ImpController impController);
    public event ImpSelected OnSelect;

    JOB job;
    
    void Awake()
    {
        job = JOB.NONE;
    }

    void OnMouseDown()
    {
        if (OnSelect != null)
        {
            OnSelect(this);
        }
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