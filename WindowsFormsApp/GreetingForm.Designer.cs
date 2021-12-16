namespace WindowsFormsApp
{
    partial class GreetingForm
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
            this.saluteButton = new System.Windows.Forms.Button();
            this.saluteTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // saluteButton
            // 
            this.saluteButton.Location = new System.Drawing.Point(326, 224);
            this.saluteButton.Name = "saluteButton";
            this.saluteButton.Size = new System.Drawing.Size(156, 49);
            this.saluteButton.TabIndex = 0;
            this.saluteButton.Text = "Salute";
            this.saluteButton.UseVisualStyleBackColor = true;
            this.saluteButton.Click += new System.EventHandler(this.saluteButton_Click);
            // 
            // saluteTextBox
            // 
            this.saluteTextBox.Location = new System.Drawing.Point(216, 153);
            this.saluteTextBox.Name = "saluteTextBox";
            this.saluteTextBox.Size = new System.Drawing.Size(374, 22);
            this.saluteTextBox.TabIndex = 1;
            // 
            // GreetingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.saluteTextBox);
            this.Controls.Add(this.saluteButton);
            this.Name = "GreetingForm";
            this.Text = "GreetingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saluteButton;
        private System.Windows.Forms.TextBox saluteTextBox;
    }
}

