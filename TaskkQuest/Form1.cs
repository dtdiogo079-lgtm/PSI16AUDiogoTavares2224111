using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TaskQuest
{
    public partial class Form1 : Form
    {
        private DatabaseHelper db;

        public Form1()
        {
            InitializeComponent();
            db = new DatabaseHelper();
        }

        
        private void btnLogin_Click(object sender, EventArgs e)
        {





            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Por favor, insira o seu email!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Por favor, insira a sua password!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            
            if (FazerLogin(txtEmail.Text, txtPassword.Text))
            {
                MessageBox.Show("Login bem-sucedido! ✅", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                int userId = GetUserID(txtEmail.Text);

                
                FormPrincipal formPrincipal = new FormPrincipal(userId, txtEmail.Text);
                formPrincipal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Email ou password incorretos! ❌", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

 
        private bool FazerLogin(string email, string password)
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT COUNT(*) FROM Utilizadores WHERE Email = @Email AND PasswordHash = @Password";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao fazer login: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        
        private int GetUserID(string email)
        {  
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT UserID FROM Utilizadores WHERE Email = @Email";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    return (int)cmd.ExecuteScalar();
                }
            }
            catch
            {
                return -1;
            }
        }

        
        private void btnRegistar_Click(object sender, EventArgs e)
        {
            FormRegisto formRegisto = new FormRegisto();
            formRegisto.ShowDialog();
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}