﻿/********************
* Name: Patrick Guo
* ID: 11378369
********************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public class VariableNode : Node
    {
        public VariableNode(string value) : base(value) { }

        public override double Eval(Dictionary<string, double> var_table)
        {
            return Convert.ToDouble(var_table[m_value]);
        }
    }
}
