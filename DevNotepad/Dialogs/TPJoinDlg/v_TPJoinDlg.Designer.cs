namespace DevNotepad.Dialogs.TPJoinDlg
{
    partial class v_TPJoinDlg
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
            lblSeparator = new Label();
            txtSeparator = new TextBox();
            lblGroupSize = new Label();
            txtGroupSize = new TextBox();
            chkTemplate = new CheckBox();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new Point(127, 135);
            btnOK.Margin = new Padding(6, 5, 6, 5);
            btnOK.Size = new Size(146, 45);
            btnOK.TabIndex = 5;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(281, 135);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Size = new Size(146, 45);
            btnCancel.TabIndex = 6;
            // 
            // lblSeparator
            // 
            lblSeparator.AutoSize = true;
            lblSeparator.Location = new Point(8, 11);
            lblSeparator.Name = "lblSeparator";
            lblSeparator.Size = new Size(98, 25);
            lblSeparator.TabIndex = 0;
            lblSeparator.Text = "&Separator:";
            // 
            // txtSeparator
            // 
            txtSeparator.Location = new Point(128, 8);
            txtSeparator.Name = "txtSeparator";
            txtSeparator.Size = new Size(296, 32);
            txtSeparator.TabIndex = 1;
            // 
            // lblGroupSize
            // 
            lblGroupSize.AutoSize = true;
            lblGroupSize.Location = new Point(8, 51);
            lblGroupSize.Name = "lblGroupSize";
            lblGroupSize.Size = new Size(106, 25);
            lblGroupSize.TabIndex = 2;
            lblGroupSize.Text = "&Group size:";
            // 
            // txtGroupSize
            // 
            txtGroupSize.Location = new Point(128, 48);
            txtGroupSize.Name = "txtGroupSize";
            txtGroupSize.Size = new Size(96, 32);
            txtGroupSize.TabIndex = 3;
            // 
            // chkTemplate
            // 
            chkTemplate.AutoSize = true;
            chkTemplate.Location = new Point(128, 88);
            chkTemplate.Name = "chkTemplate";
            chkTemplate.Size = new Size(213, 29);
            chkTemplate.TabIndex = 4;
            chkTemplate.Text = "&Template (%1% %2%)";
            chkTemplate.UseVisualStyleBackColor = true;
            // 
            // v_TPJoinDlg
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(435, 188);
            Controls.Add(chkTemplate);
            Controls.Add(txtGroupSize);
            Controls.Add(lblGroupSize);
            Controls.Add(txtSeparator);
            Controls.Add(lblSeparator);
            Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "v_TPJoinDlg";
            Text = "v_TPJoinDlg";
            Load += v_TPJoinDlg_Load;
            Controls.SetChildIndex(btnOK, 0);
            Controls.SetChildIndex(btnCancel, 0);
            Controls.SetChildIndex(lblSeparator, 0);
            Controls.SetChildIndex(txtSeparator, 0);
            Controls.SetChildIndex(lblGroupSize, 0);
            Controls.SetChildIndex(txtGroupSize, 0);
            Controls.SetChildIndex(chkTemplate, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        internal TextBox txtSeparator;
        private Label lblGroupSize;
        internal TextBox txtGroupSize;
        internal CheckBox chkTemplate;
        internal Label lblSeparator;
    }
}
