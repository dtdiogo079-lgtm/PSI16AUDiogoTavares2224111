using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TaskQuest
{
    public partial class FormPrincipal : Form
    {
        private DatabaseHelper db;
        private int userID;
        private string userEmail;

        public FormPrincipal(int userId, string email)
        {
            InitializeComponent();
            db = new DatabaseHelper();
            userID = userId;
            userEmail = email;
            CarregarDadosUtilizador();
            ConfigurarEventos();
        }

        private void ConfigurarEventos()
        {
            btnTarefas.Click += btnTarefas_Click;
            btnMinijogos.Click += btnMinijogos_Click;
            btnCalendario.Click += btnCalendario_Click;
            btnPerfil.Click += btnPerfil_Click;
            btnLogout.Click += btnLogout_Click;
        }

        private void CarregarDadosUtilizador()
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Nome, Nivel, XP_Total, Moeda_Total FROM Utilizadores WHERE UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblUserNome.Text = $"👤 {reader["Nome"]}";
                        lblNivel.Text = $"Nível {reader["Nivel"]}";
                        lblMoedas.Text = $"💰 Moedas: {reader["Moeda_Total"]}";

                        int xpTotal = Convert.ToInt32(reader["XP_Total"]);
                        int nivel = Convert.ToInt32(reader["Nivel"]);
                        int xpNecessario = nivel * 100;
                        int xpAtual = xpTotal % 100;

                        lblXP.Text = $"XP: {xpAtual}/{xpNecessario}";
                        progressBarXP.Maximum = xpNecessario;
                        progressBarXP.Value = xpAtual;
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        public void AtualizarDadosUtilizador()
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Nome, Nivel, XP_Total, Moeda_Total FROM Utilizadores WHERE UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblUserNome.Text = $"👤 {reader["Nome"]}";
                        lblNivel.Text = $"Nível {reader["Nivel"]}";
                        lblMoedas.Text = $"💰 Moedas: {reader["Moeda_Total"]}";

                        int xpTotal = Convert.ToInt32(reader["XP_Total"]);
                        int nivel = Convert.ToInt32(reader["Nivel"]);
                        int xpNecessario = 100; 
                        int xpAtual = xpTotal % 100;

                        if (xpAtual == 0 && xpTotal > 0) xpAtual = 100;

                        lblXP.Text = $"XP: {xpAtual}/{xpNecessario}";
                        progressBarXP.Maximum = xpNecessario;
                        progressBarXP.Value = Math.Min(xpAtual, xpNecessario);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erro: " + ex.Message);
            }
        }

        private void btnTarefas_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            FormTarefas formTarefas = new FormTarefas(userID);
            formTarefas.TopLevel = false;
            formTarefas.FormBorderStyle = FormBorderStyle.None;
            formTarefas.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(formTarefas);
            formTarefas.Show();
        }

        private void btnMinijogos_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            FormMinijogos formMinijogos = new FormMinijogos(userID);
            formMinijogos.TopLevel = false;
            formMinijogos.FormBorderStyle = FormBorderStyle.None;
            formMinijogos.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(formMinijogos);
            formMinijogos.Show();
        }

        private void btnCalendario_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            FormCalendario formCalendario = new FormCalendario(userID);
            formCalendario.TopLevel = false;
            formCalendario.FormBorderStyle = FormBorderStyle.None;
            formCalendario.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(formCalendario);
            formCalendario.Show();
        }
        
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Nome, Email, Nivel, XP_Total, Moeda_Total, DataRegisto FROM Utilizadores WHERE UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string nome = reader["Nome"].ToString();
                        string email = reader["Email"].ToString();
                        int nivel = Convert.ToInt32(reader["Nivel"]);
                        int xpTotal = Convert.ToInt32(reader["XP_Total"]);
                        int moedas = Convert.ToInt32(reader["Moeda_Total"]);
                        DateTime dataRegisto = Convert.ToDateTime(reader["DataRegisto"]);

                        MessageBox.Show(
                            $"👤 PERFIL DO JOGADOR\n\n" +
                            $"Nome: {nome}\n" +
                            $"Email: {email}\n" +
                            $"🎮 Nível: {nivel}\n" +
                            $"⭐ XP Total: {xpTotal}\n" +
                            $"💰 Moedas: {moedas}\n" +
                            $"📅 Membro desde: {dataRegisto:dd/MM/yyyy}",
                            "Meu Perfil",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tens a certeza que queres sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Form1 formLogin = new Form1();
                formLogin.Show();
                this.Close();
            }
        }

        private void progressBarXP_Click(object sender, EventArgs e)
        {

        }

        private void btnTarefas_Click_1(object sender, EventArgs e)
        {

        }

        private void btnMinijogos_Click_1(object sender, EventArgs e)
        {

        }

        private void btnPerfil_Click_1(object sender, EventArgs e)
        {

        }

        private void contentPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblUserNome_Click(object sender, EventArgs e)
        {

        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}