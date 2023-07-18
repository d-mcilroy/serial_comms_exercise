namespace SerialDataReceiver
{
    partial class FormMain
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
            buttonReceive = new Button();
            SuspendLayout();
            // 
            // buttonReceive
            // 
            buttonReceive.Location = new Point(14, 14);
            buttonReceive.Margin = new Padding(4, 3, 4, 3);
            buttonReceive.Name = "buttonReceive";
            buttonReceive.Size = new Size(239, 25);
            buttonReceive.TabIndex = 0;
            buttonReceive.Text = "Receive";
            buttonReceive.UseVisualStyleBackColor = true;
            buttonReceive.Click += buttonReceive_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(267, 53);
            Controls.Add(buttonReceive);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormMain";
            Text = "Receiver";
            ResumeLayout(false);
        }

        #endregion

        private Button buttonReceive;
    }
}

