csharp TasQuestt\Forms\TaskListForm.Designer.cs
namespace TasQuestt.Forms
{
    partial class TaskListForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView listViewTasks;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnPlayMinigame;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colXP;
        private System.Windows.Forms.ColumnHeader colCoins;
        private System.Windows.Forms.ColumnHeader colDone;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.listViewTasks = new System.Windows.Forms.ListView();
            this.colId = new System.Windows.Forms.ColumnHeader();
            this.colTitle = new System.Windows.Forms.ColumnHeader();
            this.colXP = new System.Windows.Forms.ColumnHeader();
            this.colCoins = new System.Windows.Forms.ColumnHeader();
            this.colDone = new System.Windows.Forms.ColumnHeader();
            this.btnComplete = new System.Windows.Forms.Button();
            this.btnPlayMinigame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewTasks
            // 
            this.listViewTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colTitle,
            this.colXP,
            this.colCoins,
            this.colDone});
            this.listViewTasks.FullRowSelect = true;
            this.listViewTasks.GridLines = true;
            this.listViewTasks.HideSelection = false;
            this.listViewTasks.Location = new System.Drawing.Point(12, 12);
            this.listViewTasks.MultiSelect = false;
            this.listViewTasks.Name = "listViewTasks";
            this.listViewTasks.Size = new System.Drawing.Size(560, 300);
            this.listViewTasks.TabIndex = 0;
            this.listViewTasks.UseCompatibleStateImageBehavior = false;
            this.listViewTasks.View = System.Windows.Forms.View.Details;
            // 
            // colId
            // 
            this.colId.Text = "ID";
            this.colId.Width = 50;
            // 
            // colTitle
            // 
            this.colTitle.Text = "Título";
            this.colTitle.Width = 260;
            // 
            // colXP
            // 
            this.colXP.Text = "XP";
            this.colXP.Width = 60;
            // 
            // colCoins
            // 
            this.colCoins.Text = "Moedas";
            this.colCoins.Width = 80;
            // 
            // colDone
            // 
            this.colDone.Text = "Concluída";
            this.colDone.Width = 80;
            // 
            // btnComplete
            // 
            this.btnComplete.Location = new System.Drawing.Point(12, 320);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(120, 30);
            this.btnComplete.TabIndex = 1;
            this.btnComplete.Text = "Concluir Tarefa";
            this.btnComplete.UseVisualStyleBackColor = true;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // btnPlayMinigame
            // 
            this.btnPlayMinigame.Location = new System.Drawing.Point(452, 320);
            this.btnPlayMinigame.Name = "btnPlayMinigame";
            this.btnPlayMinigame.Size = new System.Drawing.Size(120, 30);
            this.btnPlayMinigame.TabIndex = 2;
            this.btnPlayMinigame.Text = "Jogar Minijogo";
            this.btnPlayMinigame.UseVisualStyleBackColor = true;
            this.btnPlayMinigame.Click += new System.EventHandler(this.btnPlayMinigame_Click);
            // 
            // TaskListForm
            // 
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.btnPlayMinigame);
            this.Controls.Add(this.btnComplete);
            this.Controls.Add(this.listViewTasks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TaskListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tarefas";
            this.ResumeLayout(false);

        }

        #endregion
    }
}