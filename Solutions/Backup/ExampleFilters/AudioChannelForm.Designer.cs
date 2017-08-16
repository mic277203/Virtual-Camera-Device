namespace ExampleFilters
{
    partial class AudioChannelForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmboChannel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select an output channel";
            // 
            // cmboChannel
            // 
            this.cmboChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboChannel.FormattingEnabled = true;
            this.cmboChannel.Location = new System.Drawing.Point(161, 18);
            this.cmboChannel.Name = "cmboChannel";
            this.cmboChannel.Size = new System.Drawing.Size(195, 21);
            this.cmboChannel.TabIndex = 1;
            this.cmboChannel.SelectedIndexChanged += new System.EventHandler(this.cmboChannel_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "NOTE: Channel will not swithed if filter active";
            // 
            // AudioChannelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 253);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmboChannel);
            this.Controls.Add(this.label1);
            this.Name = "AudioChannelForm";
            this.Text = "Properties";
            this.Title = "Properties";
            this.Load += new System.EventHandler(this.AudioChannelForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboChannel;
        private System.Windows.Forms.Label label2;
    }
}