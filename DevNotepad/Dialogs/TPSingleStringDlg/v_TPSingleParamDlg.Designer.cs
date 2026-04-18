namespace DevNotepad.Dialogs.TPSingleParamDlg
{
    partial class v_TPSingleParamDlg
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
            txtParameterText = new TextBox();
            lblHint = new Label();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new Point(485, 63);
            btnOK.Margin = new Padding(6, 5, 6, 5);
            btnOK.Size = new Size(146, 45);
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(639, 63);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Size = new Size(146, 45);
            // 
            // txtParameterText
            // 
            txtParameterText.Location = new Point(8, 16);
            txtParameterText.Margin = new Padding(5);
            txtParameterText.Name = "txtParameterText";
            txtParameterText.Size = new Size(776, 32);
            txtParameterText.TabIndex = 0;
            // 
            // lblHint
            // 
            lblHint.AutoSize = true;
            lblHint.Location = new Point(8, 56);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(68, 25);
            lblHint.TabIndex = 2;
            lblHint.Text = "lblHint";
            // 
            // v_TPSingleParamDlg
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(793, 116);
            Controls.Add(lblHint);
            Controls.Add(txtParameterText);
            Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(6, 5, 6, 5);
            Name = "v_TPSingleParamDlg";
            Text = "v_TPSingleParamDlg";
            Load += v_TPSingleParamDlg_Load;
            Controls.SetChildIndex(btnOK, 0);
            Controls.SetChildIndex(btnCancel, 0);
            Controls.SetChildIndex(txtParameterText, 0);
            Controls.SetChildIndex(lblHint, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal TextBox txtParameterText;
        internal Label lblHint;
    }
}