using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer
{
    DateTime start;
    Single elapsed = -1;
    TimeSpan duration;

    void Start(Single elapsed)
    {
        this.elapsed = elapsed;
        start = DateTime.Now;
        duration = TimeSpan.Zero;
    }

    public void Update()
    {
        if(elapsed > 0)
        {
            duration = DateTime.Now - start;
            if (duration.TotalSeconds > elapsed)
                elapsed = -1;
        }
    }

    public Boolean IsEvent()
    {
        return elapsed == 0;
    }
}
