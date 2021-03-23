namespace TugasSOFirefly.FormV
{
    partial class GP
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
            this.grid_popmember = new SourceGrid.Grid();
            this.pop_member = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.grid1 = new SourceGrid.Grid();
            this.SuspendLayout();
            // 
            // grid_popmember
            // 
            this.grid_popmember.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid_popmember.EnableSort = true;
            this.grid_popmember.Location = new System.Drawing.Point(12, 40);
            this.grid_popmember.Name = "grid_popmember";
            this.grid_popmember.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.grid_popmember.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.grid_popmember.Size = new System.Drawing.Size(313, 397);
            this.grid_popmember.TabIndex = 12;
            this.grid_popmember.TabStop = true;
            this.grid_popmember.ToolTipText = "";
            // 
            // pop_member
            // 
            this.pop_member.Location = new System.Drawing.Point(12, 12);
            this.pop_member.Name = "pop_member";
            this.pop_member.Size = new System.Drawing.Size(120, 22);
            this.pop_member.TabIndex = 13;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(138, 9);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 14;
            this.kryptonButton1.Values.Text = "btn";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // grid1
            // 
            this.grid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid1.EnableSort = true;
            this.grid1.Location = new System.Drawing.Point(331, 40);
            this.grid1.Name = "grid1";
            this.grid1.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.grid1.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.grid1.Size = new System.Drawing.Size(139, 397);
            this.grid1.TabIndex = 15;
            this.grid1.TabStop = true;
            this.grid1.ToolTipText = "";
            // 
            // GP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 449);
            this.Controls.Add(this.grid1);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.pop_member);
            this.Controls.Add(this.grid_popmember);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GP";
            this.Text = "GP";
            this.ResumeLayout(false);

        }

        #endregion

        private SourceGrid.Grid grid_popmember;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown pop_member;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private SourceGrid.Grid grid1;
    }
}