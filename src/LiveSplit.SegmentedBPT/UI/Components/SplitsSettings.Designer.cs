using System.ComponentModel;

namespace LiveSplit.UI.Components
{
    partial class SplitsSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tblMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dropDownSplits = new System.Windows.Forms.ComboBox();
            this.tblSelectedSplits = new System.Windows.Forms.TableLayoutPanel();
            this.labelSelectedSegmentsInfo = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tblMainLayout.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblSelectedSplits.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tblMainLayout);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 159);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // tblMainLayout
            // 
            this.tblMainLayout.ColumnCount = 3;
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.7619F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tblMainLayout.Controls.Add(this.label1, 0, 0);
            this.tblMainLayout.Controls.Add(this.txtName, 1, 0);
            this.tblMainLayout.Controls.Add(this.tableLayoutPanel2, 1, 3);
            this.tblMainLayout.Controls.Add(this.label2, 0, 1);
            this.tblMainLayout.Controls.Add(this.btnAdd, 2, 2);
            this.tblMainLayout.Controls.Add(this.dropDownSplits, 1, 2);
            this.tblMainLayout.Controls.Add(this.tblSelectedSplits, 1, 1);
            this.tblMainLayout.Location = new System.Drawing.Point(3, 16);
            this.tblMainLayout.Name = "tblMainLayout";
            this.tblMainLayout.RowCount = 4;
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tblMainLayout.Size = new System.Drawing.Size(409, 137);
            this.tblMainLayout.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "Splits File:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tblMainLayout.SetColumnSpan(this.txtName, 2);
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(83, 4);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(323, 20);
            this.txtName.TabIndex = 46;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tblMainLayout.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.55814F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.44186F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel2.Controls.Add(this.btnMoveDown, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnMoveUp, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnRemove, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(83, 105);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(323, 29);
            this.tableLayoutPanel2.TabIndex = 48;
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.Location = new System.Drawing.Point(121, 3);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(75, 23);
            this.btnMoveDown.TabIndex = 0;
            this.btnMoveDown.Text = "Down";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUp.Location = new System.Drawing.Point(202, 3);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(36, 23);
            this.btnMoveUp.TabIndex = 1;
            this.btnMoveUp.Text = "Up";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(244, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(76, 23);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 47;
            this.label2.Text = "Segments:";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(359, 79);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(45, 20);
            this.btnAdd.TabIndex = 49;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dropDownSplits
            // 
            this.dropDownSplits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dropDownSplits.FormattingEnabled = true;
            this.dropDownSplits.Location = new System.Drawing.Point(83, 79);
            this.dropDownSplits.Name = "dropDownSplits";
            this.dropDownSplits.Size = new System.Drawing.Size(270, 21);
            this.dropDownSplits.TabIndex = 50;
            this.dropDownSplits.SelectedIndexChanged += new System.EventHandler(this.dropDownSplits_SelectedIndexChanged);
            // 
            // tblSelectedSplits
            // 
            this.tblSelectedSplits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblSelectedSplits.ColumnCount = 1;
            this.tblMainLayout.SetColumnSpan(this.tblSelectedSplits, 2);
            this.tblSelectedSplits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblSelectedSplits.Controls.Add(this.labelSelectedSegmentsInfo, 0, 0);
            this.tblSelectedSplits.Location = new System.Drawing.Point(83, 32);
            this.tblSelectedSplits.Name = "tblSelectedSplits";
            this.tblSelectedSplits.RowCount = 1;
            this.tblSelectedSplits.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblSelectedSplits.Size = new System.Drawing.Size(323, 41);
            this.tblSelectedSplits.TabIndex = 51;
            // 
            // labelSelectedSegmentsInfo
            // 
            this.labelSelectedSegmentsInfo.Location = new System.Drawing.Point(3, 0);
            this.labelSelectedSegmentsInfo.Name = "labelSelectedSegmentsInfo";
            this.labelSelectedSegmentsInfo.Size = new System.Drawing.Size(317, 39);
            this.labelSelectedSegmentsInfo.TabIndex = 0;
            this.labelSelectedSegmentsInfo.Text = "Selected segments will be displayed here. The last segment, becoming \"Best Possib" +
    "le Time\" to the end of the run, does not need to be specified and will be genera" +
    "ted by default.";
            // 
            // SplitsSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBox1);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "SplitsSettings";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(425, 169);
            this.Load += new System.EventHandler(this.SplitsSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.tblMainLayout.ResumeLayout(false);
            this.tblMainLayout.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tblSelectedSplits.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tblSelectedSplits;

        private System.Windows.Forms.ComboBox dropDownSplits;

        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

        private System.Windows.Forms.TextBox txtName;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.TableLayoutPanel tblMainLayout;

        private System.Windows.Forms.GroupBox groupBox1;

        #endregion

        private System.Windows.Forms.Label labelSelectedSegmentsInfo;
    }
}
