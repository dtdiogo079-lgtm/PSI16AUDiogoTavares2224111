using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TaskQuest
{
    public partial class FormRegisto : Form
    {
        private DatabaseHelper db;

        public FormRegisto()
        {
            InitializeComponent();
            db = new DatabaseHelper();
        }

        private void btnCriarConta_Click(object sender, EventArgs e)
        {
       
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Insira o seu nome!");
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Insira o seu email!");
                return;
            }
            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("O e-mail deve conter @");
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Insira uma password!");
                return;
            }
            if (txtPassword.Text != txtConfirmarPassword.Text)
            {
                MessageBox.Show("As passwords não coincidem!");
                return;
            }

           


            if (EmailExiste(txtEmail.Text))
            {
                MessageBox.Show("Este email já está registado!");
                return;
            }

            
            if (CriarUtilizador())
            {
                MessageBox.Show("Conta criada com sucesso! ✅");
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao criar conta!");
            }
        }

        private bool EmailExiste(string email)
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT COUNT(*) FROM Utilizadores WHERE Email = @Email";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch
            {
                return true;
            }
        }

        private bool CriarUtilizador()
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string sql = @"INSERT INTO Utilizadores (Nome, Email, PasswordHash, Nivel, XP_Total, Moeda_Total, DataRegisto) 
                                   VALUES (@Nome, @Email, @Password, 1, 0, 0, @DataRegisto)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@DataRegisto", DateTime.Now);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                return false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}