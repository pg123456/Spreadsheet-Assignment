/********************
* Name: Patrick Guo
* ID: 11378369
********************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Xml;

namespace SpreadsheetEngine
{
    public class Spreadsheet
    {
        private class ICell : Cell
        {
            public ICell(int colIndex, int rowIndex) : base(colIndex, rowIndex) { }

            public void SetValue(string value) { this.ValueSet = value; }

            public void SetDefaultValue() { this.Text = ""; }
        }

        private ICell[,] m_cell_arr;
        protected int m_columns;
        protected int m_rows;
        protected Dictionary<Cell, List<Cell>> m_dependency_dictionary; //key: cell(variable) in the formula, value: cell containing the formula

        public event PropertyChangedEventHandler CellPropertyChanged;

        Point UIcoordToSpreadCoord(string uiCoord)
        {
            char[] colstring = uiCoord.ToCharArray(0, 1);
            char[] rowstring = uiCoord.ToCharArray(1, uiCoord.Length - 1);
            string temp = new string(rowstring);
            int colint = Convert.ToInt32(colstring[0]) - 65;
            int rowint = Convert.ToInt32(temp) - 1;

            return new Point(colint, rowint);
        }

        /******************************************************************************************
        * Mechanism:
        * If the Cell's Text starts with "="...
        * An exptree called formulaEvaluator is created.
        * Scan through the Cell's Text and look for any variables.
        * For any variable found in the cell's text, it gets updated to both
        * the formulaEvaluator(exptree)'s variable table and the spreadsheet's
        * dependency table. By update, it means that if it already exists in any
        * of the two dictionaries, it gets removed from that dictionary and re-added
        * in again. 
        * 
        * On the other hand, if the Cell's Text doesn't start with "="...
        * Set the Sender Cell's value to equal to its text.
        * 
        * Regardless of whichever of the above happens,
        * if the sender Cell exists in the DepedencyDictionary, it means that
        * there are other cells that depend on the value of this cell. In this case,
        * for each cell "c" that depends on the sender cell, do a recursive function
        * call (this function, the UpdateValueOfCell() function) with "c" as the
        * sender cell (the e isn't used, so the current "e" is just passed in since
        * it doesn't matter). (I didn't actually use the foreach statement, I used
        * a forloop instead because when I used the foreach statement the compiler
        * gave me a runtime error for modification to the foreach collection). It
        * will constantly recursively call the function until it finally a cell whose
        * value is a constant. The line after the recursive function call is the line
        * that updates the UI, so for every recursive call, a cell in the UI 
        * gets updated.
        ******************************************************************************************/
        void UpdateValueOfCell(Object sender, PropertyChangedEventArgs e)
        {
            ICell cellToEval = sender as ICell;

            if (cellToEval.Text.StartsWith("="))
            {
                string formula = cellToEval.Text.Substring(1);
                ExpTree formulaEvaluator = new ExpTree(formula);
                char[] charFormula = formula.ToCharArray();

                for (int i = 0; i < charFormula.Length; i++)
                {
                    if ((charFormula[i] >= 'A') && (charFormula[i] <= 'Z'))
                    {
                        string varToAdd = "";
                        varToAdd += Convert.ToString(charFormula[i]);
                        i++;
                        while (i < charFormula.Length && charFormula[i] >= '0' && charFormula[i] <= '9')
                        {
                            varToAdd += Convert.ToString(charFormula[i]);
                            i++;
                        }

                        Point spreadsheetCoord = UIcoordToSpreadCoord(varToAdd);
                        Cell cellToAdd = m_cell_arr[spreadsheetCoord.X, spreadsheetCoord.Y];

                        if (m_dependency_dictionary.ContainsKey(cellToAdd) == false)
                        {
                            List<Cell> newCellList = new List<Cell>();
                            newCellList.Add(cellToEval);
                            m_dependency_dictionary.Add(cellToAdd, newCellList);
                        }
                        else
                            if (!m_dependency_dictionary[cellToAdd].Contains(cellToEval))
                            m_dependency_dictionary[cellToAdd].Add(cellToEval);

                        if (formulaEvaluator.VarTable.ContainsKey(varToAdd) == true)
                            formulaEvaluator.VarTable.Remove(varToAdd);

                        double cellToAddValue = double.NaN;
                        if ("" != cellToAdd.Value && "#REF!" != cellToAdd.Value)
                            cellToAddValue = Convert.ToDouble(cellToAdd.Value);
                        if (null == cellToAdd.Value || "#REF!" == cellToAdd.Value)
                            cellToAddValue = double.NaN;
                        formulaEvaluator.VarTable.Add(varToAdd, cellToAddValue);
                    }
                }

                double evaluatedValue = formulaEvaluator.Eval();
                if (double.IsNaN(evaluatedValue))
                    cellToEval.SetValue("#REF!");
                else
                    cellToEval.SetValue(Convert.ToString(evaluatedValue));
            }
            else
                cellToEval.SetValue(cellToEval.Text);

            if (m_dependency_dictionary.ContainsKey(cellToEval))
                for (int i = 0; i < m_dependency_dictionary[cellToEval].Count; i++)
                    UpdateValueOfCell(m_dependency_dictionary[cellToEval][i], e);

            CellPropertyChanged(sender, e);
        }

        public Spreadsheet(int cols, int rows)
        {
            m_dependency_dictionary = new Dictionary<Cell, List<Cell>>();

            m_columns = cols;
            m_rows = rows;

            m_cell_arr = new ICell[cols, rows];
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    m_cell_arr[i, j] = new ICell(i, j);

                    /******************************************************************************************
                    * If the PropertyChanged event is triggered, the UpdateValueofCell function will be called
                    * Subscribe to every single cell
                    ******************************************************************************************/
                    m_cell_arr[i, j].PropertyChanged += UpdateValueOfCell;
                }
            }
        }

        public int ColumnCount
        {
            get { return m_columns; }
        }

        public int RowCount
        {
            get { return m_rows; }
        }

        private void ResetAllCells()
        {
            foreach (ICell c in m_cell_arr)
                c.SetDefaultValue();
            m_dependency_dictionary.Clear();
        }

        public void LoadFile(string fileName)
        {
            ResetAllCells();

            using (XmlTextReader r = new XmlTextReader(fileName))
            {
                int current_x = 0, current_y = 0;
                while (r.Read())
                {
                    if (r.Name == "cell")
                    {
                        current_x = Convert.ToInt32(r.GetAttribute("x"));
                        current_y = Convert.ToInt32(r.GetAttribute("y"));
                    }
                    else if (r.Name == "text")
                    {
                        if (r.IsEmptyElement)
                            m_cell_arr[current_x, current_y].Text = "";
                        else
                            m_cell_arr[current_x, current_y].Text = r.ReadString();
                    }
                }
            }
        }

        public void SaveFile(string fileName)
        {
            using (XmlTextWriter w = new XmlTextWriter(fileName, null))
            {
                w.Formatting = Formatting.Indented;
                w.Indentation = 0;
                w.WriteStartElement("spreadsheet");

                foreach (Cell c in m_cell_arr)
                {
                    if (!c.HasDefaultValue())
                    {
                        w.WriteStartElement("cell");
                        w.WriteAttributeString("x", c.ColumnIndex.ToString());
                        w.WriteAttributeString("y", c.RowIndex.ToString());
                        w.WriteElementString("text", c.Text);
                        w.WriteEndElement();
                    }
                }
                w.WriteEndElement();
            }
        }

        public Cell GetCell(int col, int row)
        {
            // Check bounds, return null if out of bounds
            if (col < m_columns && row < m_rows && col >= 0 && row >= 0)
                return m_cell_arr[col, row];
            else
                return null;
        }
    }
}
