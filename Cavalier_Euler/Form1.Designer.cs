namespace Cavalier_Euler
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button101 = new System.Windows.Forms.Button();
            this.button102 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button103 = new System.Windows.Forms.Button();
            this.button104 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(459, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // button101
            // 
            this.button101.Location = new System.Drawing.Point(276, 270);
            this.button101.Name = "button101";
            this.button101.Size = new System.Drawing.Size(194, 23);
            this.button101.TabIndex = 1;
            this.button101.Text = "button1";
            this.button101.UseVisualStyleBackColor = true;
            this.button101.Click += new System.EventHandler(this.button102_Click);
            // 
            // button102
            // 
            this.button102.Location = new System.Drawing.Point(517, 270);
            this.button102.Name = "button102";
            this.button102.Size = new System.Drawing.Size(215, 23);
            this.button102.TabIndex = 2;
            this.button102.Text = "button2";
            this.button102.UseVisualStyleBackColor = true;
            this.button102.Click += new System.EventHandler(this.button102_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(696, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // button103
            // 
            this.button103.Location = new System.Drawing.Point(557, 161);
            this.button103.Name = "button103";
            this.button103.Size = new System.Drawing.Size(174, 23);
            this.button103.TabIndex = 4;
            this.button103.Text = "button1";
            this.button103.UseVisualStyleBackColor = true;
            this.button103.Click += new System.EventHandler(this.button103_Click);
            // 
            // button104
            // 
            this.button104.Location = new System.Drawing.Point(805, 161);
            this.button104.Name = "button104";
            this.button104.Size = new System.Drawing.Size(182, 23);
            this.button104.TabIndex = 5;
            this.button104.Text = "button2";
            this.button104.UseVisualStyleBackColor = true;
            this.button104.Click += new System.EventHandler(this.button103_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 937);
            this.Controls.Add(this.button104);
            this.Controls.Add(this.button103);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button102);
            this.Controls.Add(this.button101);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button101;
        private System.Windows.Forms.Button button102;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button103;
        private System.Windows.Forms.Button button104;
    }
}

