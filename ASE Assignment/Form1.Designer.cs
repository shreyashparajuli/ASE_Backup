namespace ASE_Assignment
{
    partial class Form1
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
            Run = new Button();
            ProgramWindow = new TextBox();
            OutputWindow = new PictureBox();
            btnSaveCanvas = new Button();
            btnClearCanvas = new Button();
            ((System.ComponentModel.ISupportInitialize)OutputWindow).BeginInit();
            SuspendLayout();
            // 
            // Run
            // 
            Run.Location = new Point(10, 11);
            Run.Name = "Run";
            Run.Size = new Size(81, 26);
            Run.TabIndex = 0;
            Run.Text = "Run";
            Run.UseVisualStyleBackColor = true;
            Run.Click += Run_Click;
            // 
            // ProgramWindow
            // 
            ProgramWindow.Location = new Point(10, 54);
            ProgramWindow.Multiline = true;
            ProgramWindow.Name = "ProgramWindow";
            ProgramWindow.Size = new Size(190, 422);
            ProgramWindow.TabIndex = 1;
            // 
            // OutputWindow
            // 
            OutputWindow.BackColor = SystemColors.ActiveBorder;
            OutputWindow.Location = new Point(215, 54);
            OutputWindow.Name = "OutputWindow";
            OutputWindow.Size = new Size(756, 421);
            OutputWindow.TabIndex = 2;
            OutputWindow.TabStop = false;
            OutputWindow.Paint += OutputWindow_Paint;
            // 
            // btnSaveCanvas
            // 
            btnSaveCanvas.Location = new Point(112, 13);
            btnSaveCanvas.Name = "btnSaveCanvas";
            btnSaveCanvas.Size = new Size(75, 23);
            btnSaveCanvas.TabIndex = 3;
            btnSaveCanvas.Text = "Save";
            btnSaveCanvas.UseVisualStyleBackColor = true;
            // 
            // btnClearCanvas
            // 
            btnClearCanvas.Location = new Point(215, 13);
            btnClearCanvas.Name = "btnClearCanvas";
            btnClearCanvas.Size = new Size(75, 23);
            btnClearCanvas.TabIndex = 4;
            btnClearCanvas.Text = "Clear";
            btnClearCanvas.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(983, 487);
            Controls.Add(btnClearCanvas);
            Controls.Add(btnSaveCanvas);
            Controls.Add(OutputWindow);
            Controls.Add(ProgramWindow);
            Controls.Add(Run);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)OutputWindow).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Run;
        private TextBox ProgramWindow;
        private PictureBox OutputWindow;
        private Button btnSaveCanvas;
        private Button btnClearCanvas;
    }
}
