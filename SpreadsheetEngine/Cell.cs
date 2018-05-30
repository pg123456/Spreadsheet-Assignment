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

namespace SpreadsheetEngine
{
    public abstract class Cell: INotifyPropertyChanged
    {
        protected readonly int m_cell_row_i;
        protected readonly int m_cell_column_i;
        protected string m_cell_text;
        protected string m_cell_value;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool HasDefaultValue()
        {
            return null == m_cell_text || "" == m_cell_text;
        }
        
        public Cell(int colIndex, int rowIndex)
        {
            m_cell_row_i = rowIndex;
            m_cell_column_i = colIndex;

            int[,] sdf = new int[2, 2];
        }

        public int RowIndex
        {
            get { return m_cell_row_i; }
        }

        public int ColumnIndex
        {
            get { return m_cell_column_i; }
        }

        public string Text
        {
            get { return m_cell_text; }
            set
            {
                if (m_cell_text != value)
                {
                    m_cell_text = value;

                    //if Text changes, trigger this event so spreadsheet can see
                    PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        public string Value
        {
            get { return m_cell_value; }
        }

        protected string ValueSet
        {
            set { m_cell_value = value; }
        }
    }
}
