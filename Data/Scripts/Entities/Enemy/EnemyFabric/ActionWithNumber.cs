using System;
using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ActionWithNumber
{
    public Action Action { get; set; }
    public int Number { get; set; }

    public ActionWithNumber(Action action, int number)
    {
        Action = action;
        Number = number;
    }
}
