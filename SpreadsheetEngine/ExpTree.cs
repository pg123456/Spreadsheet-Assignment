/********************
* Name: Patrick Guo
* ID: 11378369
********************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace SpreadsheetEngine
{
    public class ExpTree
    {
        private string m_current_expression;
        private Node m_root;
        private Dictionary<string, double> m_var_table;

        public ExpTree(string expression)
        {
            m_var_table = new Dictionary<string, double>();
            ParseExpression(expression);
        }

        /************************************************************
        * Every time a new expression is entered,
        * kill the old tree and make a new one
        ************************************************************/
        private void ParseExpression(string expression)
        {
            m_current_expression = expression;
            m_root = ConstructTree(expression, false);
        }

        /************************************************************
        * This is a recursive function which builds the entire
        * expression tree from the bottom to the top. 
        * Here's how the general idea of how the function works:
        * 1. Segment the expression with "+" and "-" being 
        * the delimiters then either construct a node with 
        * those segments or build a tree out of it (by calling this 
        * recursive function) depending on which one's the 
        * appropriate option and connect those nodes/trees together
        * appropriately but don't segment anything within parenthesis.
        * 2. Repeat #1 but use "*" and "/" as the delimiters.
        * 3. If an sub_expression with paranthesis as the first
        * and last element get passed to this function, go back 
        * to #1. 
        * 4. Eventually a tree will be constructed from the
        * bottom to the top. 
        ************************************************************/
        private Node ConstructTree(string sub_expression, bool using_multiply_divide_operators)
        {
            List<string> expression_pieces = new List<string>();
            char[] contents = sub_expression.ToCharArray();
            char operator1 = '+', operator2 = '-';

            if ('(' == contents[0] && ')' == contents[contents.Length - 1])
            {
                sub_expression = sub_expression.Substring(1, sub_expression.Length - 2);
                contents = sub_expression.ToCharArray();
                using_multiply_divide_operators = false; 
            }

            if (using_multiply_divide_operators)
            {
                operator1 = '*';
                operator2 = '/';
            }

            /************************************************************
            * Base Case: Expression has no parenthesis and has
            * no + or - operators
            ************************************************************/
            if (!sub_expression.Contains("+") &&
                !sub_expression.Contains("-") &&
                !sub_expression.Contains("("))
            {
                string[] split_pieces_from_multiply_operator = sub_expression.Split('*');
                List<string> split_pieces_from_multiply_operator_list = new List<string>();
                for (int i = 0; i < split_pieces_from_multiply_operator.Length; i++)
                {
                    split_pieces_from_multiply_operator_list.Add(split_pieces_from_multiply_operator[i]);
                    split_pieces_from_multiply_operator_list.Add("*");
                }
                split_pieces_from_multiply_operator_list.RemoveAt(split_pieces_from_multiply_operator_list.Count - 1);
                for (int i = 0; i < split_pieces_from_multiply_operator_list.Count; i++)
                {
                    if (split_pieces_from_multiply_operator_list[i].Contains("/"))
                    {
                        string[] split_pieces_from_divide_operator = split_pieces_from_multiply_operator_list[i].Split('/');
                        List<string> split_pieces_from_divide_operator_list = new List<string>();
                        for (int j = 0; j < split_pieces_from_divide_operator.Length; j++)
                        {
                            expression_pieces.Add(split_pieces_from_divide_operator[j]);
                            expression_pieces.Add("/");
                        }
                        expression_pieces.RemoveAt(expression_pieces.Count - 1);
                    }
                    else
                        expression_pieces.Add(split_pieces_from_multiply_operator_list[i]);
                }
            }
            else
            {
                int cut_start_i = 0;
                for (int i = 0; i < contents.Length; i++)
                {
                    if ('(' == contents[i])
                    {
                        int parenthesis_start_i = i;
                        int parenthesis_count = 1;
                        while (0 != parenthesis_count)
                        {
                            i++;
                            if ('(' == contents[i])
                                parenthesis_count++;
                            else if (')' == contents[i])
                                parenthesis_count--;
                        }
                    }
                    else if (operator1 == contents[i] || operator2 == contents[i])
                    {
                        expression_pieces.Add(sub_expression.Substring(cut_start_i, i - cut_start_i));
                        expression_pieces.Add(Convert.ToString(contents[i]));
                        cut_start_i = i + 1;
                    }
                }
                expression_pieces.Add(sub_expression.Substring(cut_start_i, sub_expression.Length - cut_start_i));
            }

            Node current_left_node = MakeNodeWithValue(expression_pieces[0]);
            if (null == current_left_node)
                current_left_node = ConstructTree(expression_pieces[0], true);
            for (int i = 1; i < expression_pieces.Count; i+=2)
            {
                OperatorNode current_operator = new OperatorNode(expression_pieces[i]);
                Node current_right_node = MakeNodeWithValue(expression_pieces[i + 1]);
                if (null == current_right_node)
                    current_right_node = ConstructTree(expression_pieces[i + 1], true);
                current_operator.Left = current_left_node;
                current_operator.Right = current_right_node;
                current_left_node = current_operator;
            }

            return current_left_node;
        }

        /************************************************************
        * Determines what kind of node to make based on the
        * substring passed to the value parameter.
        * If it starts with a letter, it's a VariableNode.
        * The tree builds upwards in one direction so the 
        * tree will usually be "unbalanced." Returns
        * null if it can't make a valid Node. 
        ************************************************************/
        private Node MakeNodeWithValue(string value)
        {
            if (GeneralHelper.StringIsOperator(value))
                return new OperatorNode(value);
            else if (GeneralHelper.StringIsVariable(value)) 
                return new VariableNode(value);
            else if (GeneralHelper.StringIsDouble(value))
                return new ValueNode(value);

            return null;
        }

        public Dictionary<string, double> VarTable
        {
            get { return m_var_table; }
        }

        private void SetVar(string varName, double varValue)
        {
            m_var_table[varName] = varValue;
        }

        /************************************************************
        * This just calls the root's subtree Eval() function
        ************************************************************/
        public double Eval()
        {
            return m_root.Eval(m_var_table);
        }
    }
}
