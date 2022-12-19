using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CrudJogador
{
    /// <summary>
    /// Lógica interna para Page1.xaml
    /// </summary>
    public partial class Page1 : Window
    {
        public Page1()
        {
            InitializeComponent();
            TXBnome.IsEnabled= false;
            TXBemail.IsEnabled= false;
            TXBlevel.IsEnabled= false;
            TXBnickname.IsEnabled= false;
            TXBRank.IsEnabled= false;
            TXBpesquisar.IsEnabled= false;
        }

        SqlConnection sqlcon = null;
        private string strcon = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=master;Data Source=DESKTOP-TMAVQ9R";
        private string strSql = string.Empty;

        private void BTNadicionar_Click(object sender, RoutedEventArgs e)
        {
            
            TXBnome.IsEnabled = true;
            TXBemail.IsEnabled = true;
            TXBlevel.IsEnabled = true;
            TXBnickname.IsEnabled = true;
            TXBRank.IsEnabled = true;
            TXBpesquisar.IsEnabled = true;
        }

        private void BTNbuscar_Click(object sender, RoutedEventArgs e)
        {
            strSql = "select*from CrudJoga where Nome=@buscar ";

            sqlcon = new SqlConnection(strcon);
            SqlCommand comando = new SqlCommand(strSql, sqlcon);

            comando.Parameters.Add("@buscar", SqlDbType.VarChar).Value = TXBpesquisar.Text;

            try
            {
                if(TXBpesquisar.Text == string.Empty)
                {
                    MessageBox.Show("Nada foi digitado");
                }
                sqlcon.Open();
                SqlDataReader dr = comando.ExecuteReader();

                if(dr.HasRows == false)
                {
                    throw new Exception("Não existe");
                }
                dr.Read();

                TXBnome.Text = Convert.ToString(dr["Nome"]);
                TXBemail.Text = Convert.ToString(dr["Email"]);
                TXBlevel.Text = Convert.ToString(dr["Nível"]);
                TXBnickname.Text = Convert.ToString(dr["Nickname"]);
                TXBRank.Text = Convert.ToString(dr["Rank"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            { 
                sqlcon.Close();
            }

            TXBpesquisar.Clear();
        }

        private void BTNsalvar_Click(object sender, RoutedEventArgs e)
        {
            strSql = "insert into CrudJoga (Nome,Email,Nickname,Nível,Rank) values(@Nome,@Email,@Nickname,@Nível,@Rank)";

            sqlcon = new SqlConnection(strcon);
            SqlCommand comando = new SqlCommand(strSql, sqlcon);

            comando.Parameters.Add("@Nome",SqlDbType.VarChar).Value = TXBnome.Text;
            comando.Parameters.Add("@Email",SqlDbType.VarChar).Value = TXBemail.Text;
            comando.Parameters.Add("@Nickname",SqlDbType.VarChar).Value = TXBnickname.Text;
            comando.Parameters.Add("@Nível",SqlDbType.VarChar).Value = TXBlevel.Text;
            comando.Parameters.Add("@Rank",SqlDbType.VarChar).Value = TXBRank.Text;
            
            try
            {
                sqlcon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Salvo");
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally 
            { 
                sqlcon.Close(); 
            }

            TXBpesquisar.IsEnabled = true;

            TXBnome.Clear();
            TXBemail.Clear();
            TXBlevel.Clear();
            TXBnickname.Clear();
            TXBRank.Clear();
            TXBpesquisar.Clear();


        }

        private void BTNeditar_Click(object sender, RoutedEventArgs e)
        {
            strSql = "update CrudJoga set Nome=@Nome, Email=@Email, Nickname=@Nickname, Nível=@Nível, Rank=@Rank";

            sqlcon = new SqlConnection(strcon);
            SqlCommand comando = new SqlCommand(strSql, sqlcon);

            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = TXBnome.Text;
            comando.Parameters.Add("@Email", SqlDbType.VarChar).Value = TXBemail.Text;
            comando.Parameters.Add("@Nickname", SqlDbType.VarChar).Value = TXBnickname.Text;
            comando.Parameters.Add("@Nível", SqlDbType.VarChar).Value = TXBlevel.Text;
            comando.Parameters.Add("@Rank", SqlDbType.VarChar).Value = TXBRank.Text;

            try
            {
                sqlcon.Open();

                comando.ExecuteNonQuery();

                MessageBox.Show("Cadastro alterado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }

            TXBnome.Clear();
            TXBemail.Clear();
            TXBlevel.Clear();
            TXBnickname.Clear();
            TXBRank.Clear();
            TXBpesquisar.Clear();
        }

        private void BTNexcluir_Click(object sender, RoutedEventArgs e)
        {
            strSql = "delete from CrudJoga where Nome=@Nome";

            sqlcon = new SqlConnection(strcon);
            SqlCommand comando = new SqlCommand(strSql, sqlcon);

            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = TXBnome.Text;

            try
            {
                sqlcon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Excluido");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }

            TXBnome.Clear();
            TXBemail.Clear();
            TXBlevel.Clear();
            TXBnickname.Clear();
            TXBRank.Clear();
            TXBpesquisar.Clear();
        }

        

    }
}
