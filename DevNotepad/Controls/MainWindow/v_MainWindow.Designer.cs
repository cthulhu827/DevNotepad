namespace DevNotepad.Controls.MainWindow
{
    partial class v_MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(v_MainWindow));
            panel1 = new Panel();
            button1 = new Button();
            tbPages = new ToolBar.ToolBar();
            tmrTimer = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 402);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 48);
            panel1.TabIndex = 1;
            panel1.Visible = false;
            // 
            // button1
            // 
            button1.Location = new Point(24, 8);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // tbPages
            // 
            tbPages.Dock = DockStyle.Top;
            tbPages.Location = new Point(0, 0);
            tbPages.Name = "tbPages";
            tbPages.Size = new Size(800, 42);
            tbPages.TabIndex = 2;
            // 
            // tmrTimer
            // 
            tmrTimer.Interval = 500;
            // 
            // v_MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tbPages);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "v_MainWindow";
            Text = "Developer Notepad";
            WindowState = FormWindowState.Maximized;
            Load += v_MainWindow_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        internal Panel panel1;
        internal Button button1;
        internal ToolBar.ToolBar tbPages;
        internal System.Windows.Forms.Timer tmrTimer;
    }
}