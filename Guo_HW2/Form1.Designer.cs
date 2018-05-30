namespace Guo_HW2
{
    partial class ui_main_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ui_data_grid_view = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ui_menu_strip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ui_load_tool_strip_menu_strip = new System.Windows.Forms.ToolStripMenuItem();
            this.ui_save_tool_strip_menu_strip = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ui_data_grid_view)).BeginInit();
            this.ui_menu_strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ui_data_grid_view
            // 
            this.ui_data_grid_view.AllowUserToAddRows = false;
            this.ui_data_grid_view.AllowUserToDeleteRows = false;
            this.ui_data_grid_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ui_data_grid_view.Location = new System.Drawing.Point(12, 62);
            this.ui_data_grid_view.Name = "ui_data_grid_view";
            this.ui_data_grid_view.RowHeadersWidth = 75;
            this.ui_data_grid_view.Size = new System.Drawing.Size(530, 530);
            this.ui_data_grid_view.TabIndex = 0;
            this.ui_data_grid_view.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.ui_data_grid_view_CellBeginEdit);
            this.ui_data_grid_view.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ui_data_grid_view_CellEndEdit);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(386, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Triple-click on a cell to edit its contents. Press Enter when you\'re finished edi" +
    "ting.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(261, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "If entering an expression, please start the text with \"=\"";
            // 
            // ui_menu_strip
            // 
            this.ui_menu_strip.BackColor = System.Drawing.Color.White;
            this.ui_menu_strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.ui_menu_strip.Location = new System.Drawing.Point(0, 0);
            this.ui_menu_strip.Name = "ui_menu_strip";
            this.ui_menu_strip.Size = new System.Drawing.Size(554, 24);
            this.ui_menu_strip.TabIndex = 4;
            this.ui_menu_strip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ui_load_tool_strip_menu_strip,
            this.ui_save_tool_strip_menu_strip});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // ui_load_tool_strip_menu_strip
            // 
            this.ui_load_tool_strip_menu_strip.Name = "ui_load_tool_strip_menu_strip";
            this.ui_load_tool_strip_menu_strip.Size = new System.Drawing.Size(152, 22);
            this.ui_load_tool_strip_menu_strip.Text = "Load";
            this.ui_load_tool_strip_menu_strip.Click += new System.EventHandler(this.ui_load_tool_strip_menu_strip_Click);
            // 
            // ui_save_tool_strip_menu_strip
            // 
            this.ui_save_tool_strip_menu_strip.Name = "ui_save_tool_strip_menu_strip";
            this.ui_save_tool_strip_menu_strip.Size = new System.Drawing.Size(152, 22);
            this.ui_save_tool_strip_menu_strip.Text = "Save";
            this.ui_save_tool_strip_menu_strip.Click += new System.EventHandler(this.ui_save_tool_strip_menu_strip_Click);
            // 
            // ui_main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 604);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ui_data_grid_view);
            this.Controls.Add(this.ui_menu_strip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.ui_menu_strip;
            this.Name = "ui_main_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spreadsheet Cpt S 322";
            this.Load += new System.EventHandler(this.ui_main_form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ui_data_grid_view)).EndInit();
            this.ui_menu_strip.ResumeLayout(false);
            this.ui_menu_strip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ui_data_grid_view;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip ui_menu_strip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ui_load_tool_strip_menu_strip;
        private System.Windows.Forms.ToolStripMenuItem ui_save_tool_strip_menu_strip;
    }
}

