using System.ComponentModel;

namespace LiveSplit.UI.Components
{
    partial class SelectedSegmentSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.labelAlias = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRemove = new System.Windows.Forms.Button();
            this.checkBoxFullAlias = new System.Windows.Forms.CheckBox();
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBoxMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.50165F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.49835F));
            this.tableLayoutPanel1.Controls.Add(this.txtAlias, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelAlias, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(305, 56);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtAlias
            // 
            this.txtAlias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAlias.Location = new System.Drawing.Point(53, 3);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(249, 20);
            this.txtAlias.TabIndex = 2;
            this.txtAlias.TextChanged += new System.EventHandler(this.txtAlias_TextChanged);
            // 
            // labelAlias
            // 
            this.labelAlias.Location = new System.Drawing.Point(3, 0);
            this.labelAlias.Name = "labelAlias";
            this.labelAlias.Size = new System.Drawing.Size(44, 23);
            this.labelAlias.TabIndex = 3;
            this.labelAlias.Text = "Alias:";
            this.labelAlias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnRemove, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxFullAlias, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(53, 30);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(249, 45);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(171, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // checkBoxFullAlias
            // 
            this.checkBoxFullAlias.AutoSize = true;
            this.checkBoxFullAlias.Location = new System.Drawing.Point(3, 3);
            this.checkBoxFullAlias.Name = "checkBoxFullAlias";
            this.checkBoxFullAlias.Size = new System.Drawing.Size(67, 17);
            this.checkBoxFullAlias.TabIndex = 5;
            this.checkBoxFullAlias.Text = "Full Alias";
            this.checkBoxFullAlias.UseVisualStyleBackColor = true;
            this.checkBoxFullAlias.CheckedChanged += new System.EventHandler(this.checkBoxFullAlias_CheckedChanged);
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxMain.Location = new System.Drawing.Point(3, 3);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Size = new System.Drawing.Size(311, 75);
            this.groupBoxMain.TabIndex = 0;
            this.groupBoxMain.TabStop = false;
            this.groupBoxMain.Text = "#5: SSL (26)";
            // 
            // SelectedSegmentSettings
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBoxMain);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "SelectedSegmentSettings";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(320, 83);
            this.Load += new System.EventHandler(this.SelectedSegmentSettings_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBoxMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnRemove;

        private System.Windows.Forms.Label labelAlias;

        private System.Windows.Forms.GroupBox groupBoxMain;

        private System.Windows.Forms.TextBox txtAlias;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBoxFullAlias;
    }
}
