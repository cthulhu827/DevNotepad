namespace DevNotepad.Controls.WorkArea
{
    partial class v_WorkArea
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
            if (disposing )
            {
                if (components != null) components.Dispose();

                if (isResizing) EndResize(true);
                UninstallEscapeCancelFilter();
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
            txtSource = new ScrollableTextBox();
            txtTransformed = new ScrollableTextBox();
            pipeControl = new PipeControl.PipeControl();
            lblSource = new Label();
            lblTransformed = new Label();
            SuspendLayout();
            // 
            // txtSource
            // 
            txtSource.BorderStyle = BorderStyle.None;
            txtSource.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtSource.Location = new Point(40, 80);
            txtSource.Multiline = true;
            txtSource.Name = "txtSource";
            txtSource.Size = new Size(128, 120);
            txtSource.TabIndex = 0;
            txtSource.WordWrap = false;
            // 
            // txtTransformed
            // 
            txtTransformed.BorderStyle = BorderStyle.None;
            txtTransformed.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtTransformed.Location = new Point(248, 72);
            txtTransformed.Multiline = true;
            txtTransformed.Name = "txtTransformed";
            txtTransformed.ReadOnly = true;
            txtTransformed.Size = new Size(128, 120);
            txtTransformed.TabIndex = 1;
            txtTransformed.WordWrap = false;
            // 
            // pipeControl
            // 
            pipeControl.Dock = DockStyle.Top;
            pipeControl.Location = new Point(0, 0);
            pipeControl.Name = "pipeControl";
            pipeControl.Size = new Size(476, 64);
            pipeControl.TabIndex = 2;
            // 
            // lblSource
            // 
            lblSource.Location = new Point(64, 224);
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(100, 23);
            lblSource.TabIndex = 3;
            lblSource.Text = "lblSource";
            lblSource.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTransformed
            // 
            lblTransformed.Location = new Point(280, 224);
            lblTransformed.Name = "lblTransformed";
            lblTransformed.Size = new Size(100, 23);
            lblTransformed.TabIndex = 4;
            lblTransformed.Text = "lblTransformed";
            lblTransformed.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // v_WorkArea
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            Controls.Add(lblTransformed);
            Controls.Add(lblSource);
            Controls.Add(pipeControl);
            Controls.Add(txtTransformed);
            Controls.Add(txtSource);
            Name = "v_WorkArea";
            Size = new Size(476, 264);
            Load += v_WorkArea_Load;
            Resize += v_WorkArea_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal ScrollableTextBox txtSource;
        internal ScrollableTextBox txtTransformed;
        internal PipeControl.PipeControl pipeControl;
        internal Label lblSource;
        internal Label lblTransformed;
    }
}
