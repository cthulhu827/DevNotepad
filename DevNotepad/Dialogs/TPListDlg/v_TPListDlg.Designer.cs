namespace DevNotepad.Dialogs.TPListDlg
{
    partial class v_TPListDlg
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
            lblOperation = new Label();
            rbExclude = new RadioButton();
            rbIntersect = new RadioButton();
            rbUnion = new RadioButton();
            rbConcat = new RadioButton();
            lblList1 = new Label();
            lblList2 = new Label();
            lbList1 = new Controls.StringLists.lb_StringList();
            lbList2 = new Controls.StringLists.lb_StringList();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new Point(319, 215);
            btnOK.Margin = new Padding(6, 5, 6, 5);
            btnOK.Size = new Size(146, 45);
            btnOK.TabIndex = 9;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(473, 215);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Size = new Size(146, 45);
            btnCancel.TabIndex = 10;
            // 
            // lblOperation
            // 
            lblOperation.AutoSize = true;
            lblOperation.Location = new Point(8, 136);
            lblOperation.Name = "lblOperation";
            lblOperation.Size = new Size(101, 25);
            lblOperation.TabIndex = 4;
            lblOperation.Text = "Operation:";
            // 
            // rbExclude
            // 
            rbExclude.AutoSize = true;
            rbExclude.Location = new Point(32, 168);
            rbExclude.Name = "rbExclude";
            rbExclude.Size = new Size(95, 29);
            rbExclude.TabIndex = 5;
            rbExclude.TabStop = true;
            rbExclude.Text = "&Exclude";
            rbExclude.UseVisualStyleBackColor = true;
            // 
            // rbIntersect
            // 
            rbIntersect.AutoSize = true;
            rbIntersect.Location = new Point(144, 168);
            rbIntersect.Name = "rbIntersect";
            rbIntersect.Size = new Size(102, 29);
            rbIntersect.TabIndex = 6;
            rbIntersect.TabStop = true;
            rbIntersect.Text = "&Intersect";
            rbIntersect.UseVisualStyleBackColor = true;
            // 
            // rbUnion
            // 
            rbUnion.AutoSize = true;
            rbUnion.Location = new Point(264, 168);
            rbUnion.Name = "rbUnion";
            rbUnion.Size = new Size(81, 29);
            rbUnion.TabIndex = 7;
            rbUnion.TabStop = true;
            rbUnion.Text = "&Union";
            rbUnion.UseVisualStyleBackColor = true;
            // 
            // rbConcat
            // 
            rbConcat.AutoSize = true;
            rbConcat.Location = new Point(368, 168);
            rbConcat.Name = "rbConcat";
            rbConcat.Size = new Size(89, 29);
            rbConcat.TabIndex = 8;
            rbConcat.TabStop = true;
            rbConcat.Text = "&Concat";
            rbConcat.UseVisualStyleBackColor = true;
            // 
            // lblList1
            // 
            lblList1.AutoSize = true;
            lblList1.Location = new Point(8, 8);
            lblList1.Name = "lblList1";
            lblList1.Size = new Size(54, 25);
            lblList1.TabIndex = 0;
            lblList1.Text = "List&1:";
            // 
            // lblList2
            // 
            lblList2.AutoSize = true;
            lblList2.Location = new Point(320, 8);
            lblList2.Name = "lblList2";
            lblList2.Size = new Size(54, 25);
            lblList2.TabIndex = 2;
            lblList2.Text = "List&2:";
            // 
            // lbList1
            // 
            lbList1.BorderStyle = BorderStyle.FixedSingle;
            lbList1.FocusedBorderColor = Color.FromArgb(94, 181, 247);
            lbList1.FormattingEnabled = true;
            lbList1.ItemHeight = 25;
            lbList1.Location = new Point(8, 40);
            lbList1.Margin = new Padding(5);
            lbList1.Name = "lbList1";
            lbList1.Size = new Size(298, 77);
            lbList1.TabIndex = 1;
            lbList1.UnfocusedBorderColor = Color.FromArgb(37, 48, 62);
            // 
            // lbList2
            // 
            lbList2.BorderStyle = BorderStyle.FixedSingle;
            lbList2.FocusedBorderColor = Color.FromArgb(94, 181, 247);
            lbList2.FormattingEnabled = true;
            lbList2.ItemHeight = 25;
            lbList2.Location = new Point(320, 40);
            lbList2.Margin = new Padding(5);
            lbList2.Name = "lbList2";
            lbList2.Size = new Size(298, 77);
            lbList2.TabIndex = 3;
            lbList2.UnfocusedBorderColor = Color.FromArgb(37, 48, 62);
            // 
            // v_TPListDlg
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(627, 268);
            Controls.Add(lbList2);
            Controls.Add(lbList1);
            Controls.Add(lblList2);
            Controls.Add(lblList1);
            Controls.Add(rbConcat);
            Controls.Add(rbUnion);
            Controls.Add(rbIntersect);
            Controls.Add(rbExclude);
            Controls.Add(lblOperation);
            Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "v_TPListDlg";
            Text = "v_TPListDlg";
            Load += v_TPListDlg_Load;
            Controls.SetChildIndex(btnOK, 0);
            Controls.SetChildIndex(btnCancel, 0);
            Controls.SetChildIndex(lblOperation, 0);
            Controls.SetChildIndex(rbExclude, 0);
            Controls.SetChildIndex(rbIntersect, 0);
            Controls.SetChildIndex(rbUnion, 0);
            Controls.SetChildIndex(rbConcat, 0);
            Controls.SetChildIndex(lblList1, 0);
            Controls.SetChildIndex(lblList2, 0);
            Controls.SetChildIndex(lbList1, 0);
            Controls.SetChildIndex(lbList2, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblOperation;
        internal RadioButton rbExclude;
        internal RadioButton rbIntersect;
        internal RadioButton rbUnion;
        internal RadioButton rbConcat;
        private Label lblList1;
        private Label lblList2;
        internal Controls.StringLists.lb_StringList lbList1;
        internal Controls.StringLists.lb_StringList lbList2;
    }
}
