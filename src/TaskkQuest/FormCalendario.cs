using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace TaskQuest
{
    public partial class FormCalendario : Form
    {
        private DatabaseHelper db;
        private int userID;
        private DateTime dataAtual;

        public FormCalendario(int userId)
        {
            InitializeComponent();
            db = new DatabaseHelper();
            userID = userId;
            dataAtual = DateTime.Now;
            AtualizarCalendario();
        }

        private void AtualizarCalendario()
        {
            panelCalendario.Controls.Clear();
            lblMes.Text = dataAtual.ToString("MMMM yyyy").ToUpper();

            string[] diasSemana = { "Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb" };
            for (int i = 0; i < 7; i++)
            {
                Label lbl = new Label();
                lbl.Text = diasSemana[i];
                lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.BackColor = Color.Black;
                lbl.ForeColor = Color.White;
                lbl.Dock = DockStyle.Fill;
                panelCalendario.Controls.Add(lbl, i, 0);
            }

            DateTime primeiroDia = new DateTime(dataAtual.Year, dataAtual.Month, 1);
            int diasNoMes = DateTime.DaysInMonth(dataAtual.Year, dataAtual.Month);
            int startIndex = (int)primeiroDia.DayOfWeek;

            var progresso = CarregarProgressoMes();

            for (int i = 1; i <= diasNoMes; i++)
            {
                int linha = (startIndex + i - 1) / 7 + 1;
                int coluna = (startIndex + i - 1) % 7;

                Button btn = new Button();
                btn.Text = i.ToString();
                btn.Font = new Font("Segoe UI", 10);
                btn.FlatStyle = FlatStyle.Flat;
                btn.Dock = DockStyle.Fill;
                btn.Tag = new DateTime(dataAtual.Year, dataAtual.Month, i);
                btn.Click += DiaClicado;

                DateTime data = new DateTime(dataAtual.Year, dataAtual.Month, i);
                if (progresso.ContainsKey(data))
                {
                    var (tarefas, xp) = progresso[data];
                    btn.BackColor = Color.LightGreen;  
                    btn.ForeColor = Color.Black;
                    btn.Text = $"{i}\n✓{tarefas}";
                    btn.Font = new Font("Segoe UI", 8);
                }
                else if (data.Date == DateTime.Now.Date)
                {
                    btn.BackColor = Color.Black;
                    btn.ForeColor = Color.White;
                }
                else
                {
                    btn.BackColor = Color.White;
                }

                panelCalendario.Controls.Add(btn, coluna, linha);
            }
        }

        private System.Collections.Generic.Dictionary<DateTime, (int, int)> CarregarProgressoMes()
        {
            var resultado = new System.Collections.Generic.Dictionary<DateTime, (int, int)>();
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Data, TarefasConcluidas, XPGanhado FROM ProgressoDiario WHERE UserID = @UserID AND YEAR(Data) = @Ano AND MONTH(Data) = @Mes";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@Ano", dataAtual.Year);
                    cmd.Parameters.AddWithValue("@Mes", dataAtual.Month);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DateTime data = reader.GetDateTime(0);
                        int tarefas = reader.GetInt32(1);
                        int xp = reader.GetInt32(2);
                        resultado[data.Date] = (tarefas, xp);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erro: " + ex.Message);
            }
            return resultado;
        }

        private void DiaClicado(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Tag != null)
            {
                DateTime data = (DateTime)btn.Tag;
                var progresso = CarregarProgressoMes();

                if (progresso.ContainsKey(data))
                {
                    var (tarefas, xp) = progresso[data];
                    MessageBox.Show($"📅 {data:dd/MM/yyyy}\n\n✅ Tarefas concluídas: {tarefas}\n⭐ XP ganho: {xp}", "Progresso do Dia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"📅 {data:dd/MM/yyyy}\n\nNenhuma tarefa concluída neste dia.\n\nContinua a trabalhar nas tuas tarefas! 💪", "Progresso do Dia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            dataAtual = dataAtual.AddMonths(-1);
            AtualizarCalendario();
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            dataAtual = dataAtual.AddMonths(1);
            AtualizarCalendario();
        }

        private void lblMes_Click(object sender, EventArgs e)
        {

        }

        private void panelCalendario_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}