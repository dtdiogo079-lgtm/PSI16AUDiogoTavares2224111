namespace TaskQuest
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.lblUserNome = new System.Windows.Forms.Label();
            this.lblNivel = new System.Windows.Forms.Label();
            this.progressBarXP = new System.Windows.Forms.ProgressBar();
            this.lblXP = new System.Windows.Forms.Label();
            this.lblMoedas = new System.Windows.Forms.Label();
            this.btnTarefas = new System.Windows.Forms.Button();
            this.btnMinijogos = new System.Windows.Forms.Button();
            this.btnCalendario = new System.Windows.Forms.Button();
            this.btnPerfil = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.sidebarPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.Black;
            this.sidebarPanel.Controls.Add(this.lblUserNome);
            this.sidebarPanel.Controls.Add(this.lblNivel);
            this.sidebarPanel.Controls.Add(this.progressBarXP);
            this.sidebarPanel.Controls.Add(this.lblXP);
            this.sidebarPanel.Controls.Add(this.lblMoedas);
            this.sidebarPanel.Controls.Add(this.btnTarefas);
            this.sidebarPanel.Controls.Add(this.btnMinijogos);
            this.sidebarPanel.Controls.Add(this.btnCalendario);
            this.sidebarPanel.Controls.Add(this.btnPerfil);
            this.sidebarPanel.Controls.Add(this.btnLogout);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Margin = new System.Windows.Forms.Padding(2);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(188, 459);
            this.sidebarPanel.TabIndex = 0;
            // 
            // lblUserNome
            // 
            this.lblUserNome.AutoSize = true;
            this.lblUserNome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblUserNome.ForeColor = System.Drawing.Color.Lavender;
            this.lblUserNome.Location = new System.Drawing.Point(30, 24);
            this.lblUserNome.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserNome.Name = "lblUserNome";
            this.lblUserNome.Size = new System.Drawing.Size(113, 21);
            this.lblUserNome.TabIndex = 0;
            this.lblUserNome.Text = "👤 Utilizador";
            this.lblUserNome.Click += new System.EventHandler(this.lblUserNome_Click);
            // 
            // lblNivel
            // 
            this.lblNivel.AutoSize = true;
            this.lblNivel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNivel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.lblNivel.Location = new System.Drawing.Point(64, 57);
            this.lblNivel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNivel.Name = "lblNivel";
            this.lblNivel.Size = new System.Drawing.Size(51, 19);
            this.lblNivel.TabIndex = 1;
            this.lblNivel.Text = "Nível 1";
            // 
            // progressBarXP
            // 
            this.progressBarXP.Location = new System.Drawing.Point(19, 85);
            this.progressBarXP.Margin = new System.Windows.Forms.Padding(2);
            this.progressBarXP.Name = "progressBarXP";
            this.progressBarXP.Size = new System.Drawing.Size(150, 16);
            this.progressBarXP.TabIndex = 2;
            this.progressBarXP.Click += new System.EventHandler(this.progressBarXP_Click);
            // 
            // lblXP
            // 
            this.lblXP.AutoSize = true;
            this.lblXP.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblXP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.lblXP.Location = new System.Drawing.Point(19, 106);
            this.lblXP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblXP.Name = "lblXP";
            this.lblXP.Size = new System.Drawing.Size(53, 13);
            this.lblXP.TabIndex = 3;
            this.lblXP.Text = "XP: 0/100";
            // 
            // lblMoedas
            // 
            this.lblMoedas.AutoSize = true;
            this.lblMoedas.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMoedas.ForeColor = System.Drawing.Color.Gold;
            this.lblMoedas.Location = new System.Drawing.Point(38, 134);
            this.lblMoedas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMoedas.Name = "lblMoedas";
            this.lblMoedas.Size = new System.Drawing.Size(107, 20);
            this.lblMoedas.TabIndex = 4;
            this.lblMoedas.Text = "💰 Moedas: 0";
            // 
            // btnTarefas
            // 
            this.btnTarefas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTarefas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnTarefas.ForeColor = System.Drawing.Color.Chartreuse;
            this.btnTarefas.Location = new System.Drawing.Point(0, 179);
            this.btnTarefas.Margin = new System.Windows.Forms.Padding(2);
            this.btnTarefas.Name = "btnTarefas";
            this.btnTarefas.Size = new System.Drawing.Size(188, 37);
            this.btnTarefas.TabIndex = 5;
            this.btnTarefas.Text = "📋 Tarefas";
            this.btnTarefas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTarefas.UseVisualStyleBackColor = true;
            this.btnTarefas.Click += new System.EventHandler(this.btnTarefas_Click_1);
            // 
            // btnMinijogos
            // 
            this.btnMinijogos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinijogos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnMinijogos.ForeColor = System.Drawing.Color.Turquoise;
            this.btnMinijogos.Location = new System.Drawing.Point(0, 223);
            this.btnMinijogos.Margin = new System.Windows.Forms.Padding(2);
            this.btnMinijogos.Name = "btnMinijogos";
            this.btnMinijogos.Size = new System.Drawing.Size(188, 37);
            this.btnMinijogos.TabIndex = 6;
            this.btnMinijogos.Text = "🎮 Minijogos";
            this.btnMinijogos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMinijogos.UseVisualStyleBackColor = true;
            this.btnMinijogos.Click += new System.EventHandler(this.btnMinijogos_Click_1);
            // 
            // btnCalendario
            // 
            this.btnCalendario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalendario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCalendario.ForeColor = System.Drawing.Color.Gold;
            this.btnCalendario.Location = new System.Drawing.Point(0, 268);
            this.btnCalendario.Margin = new System.Windows.Forms.Padding(2);
            this.btnCalendario.Name = "btnCalendario";
            this.btnCalendario.Size = new System.Drawing.Size(188, 37);
            this.btnCalendario.TabIndex = 7;
            this.btnCalendario.Text = "📅 Calendário";
            this.btnCalendario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCalendario.UseVisualStyleBackColor = true;
            // 
            // btnPerfil
            // 
            this.btnPerfil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerfil.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPerfil.ForeColor = System.Drawing.Color.Coral;
            this.btnPerfil.Location = new System.Drawing.Point(0, 313);
            this.btnPerfil.Margin = new System.Windows.Forms.Padding(2);
            this.btnPerfil.Name = "btnPerfil";
            this.btnPerfil.Size = new System.Drawing.Size(188, 37);
            this.btnPerfil.TabIndex = 8;
            this.btnPerfil.Text = "👤 Perfil";
            this.btnPerfil.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPerfil.UseVisualStyleBackColor = true;
            this.btnPerfil.Click += new System.EventHandler(this.btnPerfil_Click_1);
            // 
            // btnLogout
            // 
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnLogout.Location = new System.Drawing.Point(0, 390);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(188, 37);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "🚪 Sair";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.PaleGreen;
            this.contentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contentPanel.Controls.Add(this.label3);
            this.contentPanel.Controls.Add(this.label2);
            this.contentPanel.Controls.Add(this.pictureBox1);
            this.contentPanel.Controls.Add(this.label1);
            this.contentPanel.Controls.Add(this.lblWelcome);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(188, 0);
            this.contentPanel.Margin = new System.Windows.Forms.Padding(2);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(713, 459);
            this.contentPanel.TabIndex = 1;
            this.contentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.contentPanel_Paint);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label3.Location = new System.Drawing.Point(25, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(330, 90);
            this.label3.TabIndex = 4;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 79);
            this.label2.TabIndex = 3;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TaskkQuest.Properties.Resources.Captura_de_ecrã_2026_05_21_163818_removebg_preview;
            this.pictureBox1.Location = new System.Drawing.Point(523, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(212, 88);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DarkGreen;
            this.label1.Location = new System.Drawing.Point(233, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "TaskQuest!";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblWelcome.Location = new System.Drawing.Point(22, 24);
            this.lblWelcome.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(222, 32);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "🏠 Bem-vindo ao ";
            this.lblWelcome.Click += new System.EventHandler(this.lblWelcome_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 459);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.sidebarPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TaskQuest - Menu ";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.sidebarPanel.ResumeLayout(false);
            this.sidebarPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Label lblUserNome;
        private System.Windows.Forms.Label lblNivel;
        private System.Windows.Forms.ProgressBar progressBarXP;
        private System.Windows.Forms.Label lblXP;
        private System.Windows.Forms.Label lblMoedas;
        private System.Windows.Forms.Button btnTarefas;
        private System.Windows.Forms.Button btnMinijogos;
        private System.Windows.Forms.Button btnCalendario;
        private System.Windows.Forms.Button btnPerfil;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}