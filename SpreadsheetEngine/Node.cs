/********************
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
    public abstract class Node
    {
        protected string m_value;

        public Node(string value) { m_value = value; }

        /************************************************************
        * Gets the evaluated value of the node's subtree
        ************************************************************/
        public abstract double Eval(Dictionary<string, double> var_table);
    }
}
