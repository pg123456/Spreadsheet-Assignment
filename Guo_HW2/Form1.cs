/********************
* Name: Patrick Guo
* ID: 11378369
********************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpreadsheetEngine;

namespace Guo_HW2
{
    /******************************************************************************************
    * The main class for the UI side mainly consisting of event trigger functions. 
    ******************************************************************************************/
    public partial class ui_main_form : Form
    {
        private Spreadsheet m_my_spreadsheet;

        void UpdateGrid(Object sender, PropertyChangedEventArgs e)
        {
            Cell to_update = sender as Cell;
            ui_data_grid_view[to_update.ColumnIndex, to_update.RowIndex].Value = to_update.Value;
        }

        public ui_main_form()
        {
            InitializeComponent();

            m_my_spreadsheet = new Spreadsheet(26, 50);
            m_my_spreadsheet.CellPropertyChanged += UpdateGrid;
        }

        private void ui_main_form_Load(object sender, EventArgs e)
        {
            // load columns, convert index ascii value to char value to use as name
            for (int i = 65; i < 91; i++)
            {
                DataGridViewColumn to_add = new DataGridViewTextBoxColumn();
                to_add.Name = Convert.ToChar(i).ToString();
                to_add.HeaderText = to_add.Name;
                ui_data_grid_view.Columns.Add(to_add);
            }

            // load rows
            for (int i = 1; i < 51; i++)
            {
                DataGridViewRow to_add = new DataGridViewRow();
                to_add.HeaderCell.Value = i.ToString();
                ui_data_grid_view.Rows.Add(to_add);
            }
        }
        
        private void ui_data_grid_view_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            ui_data_grid_view[e.ColumnIndex, e.RowIndex].Value = m_my_spreadsheet.GetCell(e.ColumnIndex, e.RowIndex).Text;
        }
        
        private void ui_data_grid_view_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (null != ui_data_grid_view[e.ColumnIndex, e.RowIndex].Value)
                m_my_spreadsheet.GetCell(e.ColumnIndex, e.RowIndex).Text = ui_data_grid_view[e.ColumnIndex, e.RowIndex].Value.ToString();
            else
                m_my_spreadsheet.GetCell(e.ColumnIndex, e.RowIndex).Text = "";
            ui_data_grid_view[e.ColumnIndex, e.RowIndex].Value = m_my_spreadsheet.GetCell(e.ColumnIndex, e.RowIndex).Value;
        }

        private void ui_load_tool_strip_menu_strip_Click(object sender, EventArgs e)
        {
            OpenFileDialog file_to_open = new OpenFileDialog();
            DialogResult clicked_open = file_to_open.ShowDialog();

            if (clicked_open == DialogResult.OK)
                m_my_spreadsheet.LoadFile(file_to_open.FileName);
        }

        private void ui_save_tool_strip_menu_strip_Click(object sender, EventArgs e)
        {
            SaveFileDialog file_to_save = new SaveFileDialog();
            DialogResult clicked_save = file_to_save.ShowDialog();

            if (clicked_save == DialogResult.OK)
                m_my_spreadsheet.SaveFile(file_to_save.FileName);
        }
    }
}
