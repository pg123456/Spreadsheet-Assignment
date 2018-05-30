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
    public class OperatorNode : Node
    {
        private Node m_left;
        private Node m_right;

        public OperatorNode(string value) : base(value) { }

        public Node Left
        {
            get { return m_left; }
            set { m_left = value; }
        }

        public Node Right
        {
            get { return m_right; }
            set { m_right = value; }
        }

        public override double Eval(Dictionary<string, double> var_table)
        {
            double left_value = m_left.Eval(var_table), right_value = m_right.Eval(var_table);
            if (double.NaN == left_value || double.NaN == right_value)
                return double.NaN;

            if ("+" == m_value)
                return left_value + right_value;
            else if ("-" == m_value)
                return left_value - right_value;
            else if ("*" == m_value)
                return left_value * right_value;
            else
                return left_value / right_value;
        }
    }
}
