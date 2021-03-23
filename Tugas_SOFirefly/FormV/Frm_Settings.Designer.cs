namespace TugasSOFirefly.FormV
{
    partial class Frm_Settings
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
            this.nmr_digit = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.nmr_jeda = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.SuspendLayout();
            // 
            // nmr_digit
            // 
            this.nmr_digit.Location = new System.Drawing.Point(117, 27);
            this.nmr_digit.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nmr_digit.Name = "nmr_digit";
            this.nmr_digit.Size = new System.Drawing.Size(120, 22);
            this.nmr_digit.TabIndex = 0;
            this.nmr_digit.ValueChanged += new System.EventHandler(this.nmr_digit_ValueChanged);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(27, 27);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(84, 20);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Jumlah Digit :";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(117, 55);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(56, 20);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "(contoh)";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(70, 81);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(41, 20);
            this.kryptonLabel3.TabIndex = 4;
            this.kryptonLabel3.Values.Text = "Jeda :";
            // 
            // nmr_jeda
            // 
            this.nmr_jeda.Location = new System.Drawing.Point(117, 81);
            this.nmr_jeda.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.nmr_jeda.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nmr_jeda.Name = "nmr_jeda";
            this.nmr_jeda.Size = new System.Drawing.Size(120, 22);
            this.nmr_jeda.TabIndex = 3;
            this.nmr_jeda.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nmr_jeda.ValueChanged += new System.EventHandler(this.nmr_jeda_ValueChanged);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(117, 109);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(56, 20);
            this.kryptonLabel4.TabIndex = 5;
            this.kryptonLabel4.Values.Text = "(contoh)";
            // 
            // Frm_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 397);
            this.Controls.Add(this.kryptonLabel4);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.nmr_jeda);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.nmr_digit);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Frm_Settings";
            this.Text = "Pengaturan Utama";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown nmr_digit;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown nmr_jeda;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
    }
}