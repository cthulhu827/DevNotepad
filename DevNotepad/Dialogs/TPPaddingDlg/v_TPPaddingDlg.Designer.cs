namespace DevNotepad.Dialogs.TPPaddingDlg
{
    partial class v_TPPaddingDlg
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
            lblTotalLength = new Label();
            txtTotalLength = new TextBox();
            lblSymbol = new Label();
            txtSymbol = new TextBox();
            lblType = new Label();
            rbLeading = new RadioButton();
            rbTrailing = new RadioButton();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new Point(127, 135);
            btnOK.Margin = new Padding(6, 5, 6, 5);
            btnOK.Size = new Size(146, 45);
            btnOK.TabIndex = 6;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(281, 135);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Size = new Size(146, 45);
            btnCancel.TabIndex = 7;
            // 
            // lblTotalLength
            // 
            lblTotalLength.AutoSize = true;
            lblTotalLength.Location = new Point(8, 11);
            lblTotalLength.Name = "lblTotalLength";
            lblTotalLength.Size = new Size(115, 25);
            lblTotalLength.TabIndex = 0;
            lblTotalLength.Text = "Total &length:";
            // 
            // txtTotalLength
            // 
            txtTotalLength.Location = new Point(128, 8);
            txtTotalLength.Name = "txtTotalLength";
            txtTotalLength.Size = new Size(296, 32);
            txtTotalLength.TabIndex = 1;
            // 
            // lblSymbol
            // 
            lblSymbol.AutoSize = true;
            lblSymbol.Location = new Point(8, 51);
            lblSymbol.Name = "lblSymbol";
            lblSymbol.Size = new Size(77, 25);
            lblSymbol.TabIndex = 2;
            lblSymbol.Text = "&Symbol:";
            // 
            // txtSymbol
            // 
            txtSymbol.Location = new Point(128, 48);
            txtSymbol.MaxLength = 1;
            txtSymbol.Name = "txtSymbol";
            txtSymbol.Size = new Size(80, 32);
            txtSymbol.TabIndex = 3;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(8, 91);
            lblType.Name = "lblType";
            lblType.Size = new Size(55, 25);
            lblType.TabIndex = 4;
            lblType.Text = "Type:";
            // 
            // rbLeading
            // 
            rbLeading.AutoSize = true;
            rbLeading.Location = new Point(128, 88);
            rbLeading.Name = "rbLeading";
            rbLeading.Size = new Size(124, 29);
            rbLeading.TabIndex = 5;
            rbLeading.TabStop = true;
            rbLeading.Text = "Leading (&1)";
            rbLeading.UseVisualStyleBackColor = true;
            // 
            // rbTrailing
            // 
            rbTrailing.AutoSize = true;
            rbTrailing.Location = new Point(280, 88);
            rbTrailing.Name = "rbTrailing";
            rbTrailing.Size = new Size(119, 29);
            rbTrailing.TabIndex = 6;
            rbTrailing.TabStop = true;
            rbTrailing.Text = "Trailing (&2)";
            rbTrailing.UseVisualStyleBackColor = true;
            // 
            // v_TPPaddingDlg
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(435, 188);
            Controls.Add(rbTrailing);
            Controls.Add(rbLeading);
            Controls.Add(lblType);
            Controls.Add(txtSymbol);
            Controls.Add(lblSymbol);
            Controls.Add(txtTotalLength);
            Controls.Add(lblTotalLength);
            Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "v_TPPaddingDlg";
            Text = "v_TPPaddingDlg";
            Load += v_TPPaddingDlg_Load;
            Controls.SetChildIndex(btnOK, 0);
            Controls.SetChildIndex(btnCancel, 0);
            Controls.SetChildIndex(lblTotalLength, 0);
            Controls.SetChildIndex(txtTotalLength, 0);
            Controls.SetChildIndex(lblSymbol, 0);
            Controls.SetChildIndex(txtSymbol, 0);
            Controls.SetChildIndex(lblType, 0);
            Controls.SetChildIndex(rbLeading, 0);
            Controls.SetChildIndex(rbTrailing, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTotalLength;
        internal TextBox txtTotalLength;
        private Label lblSymbol;
        internal TextBox txtSymbol;
        private Label lblType;
        internal RadioButton rbLeading;
        internal RadioButton rbTrailing;
    }
}
