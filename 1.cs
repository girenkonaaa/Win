
using Microsoft.VisualBasic.ApplicationServices;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp1

{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

       static void ����������������������()
        {
            string log = tb_Login.Text;
            string pass = tb_Password.Text;
            if (log != "" && pass != "")
            {
                string query = $"select * from [Login].[������������]";
                using (SqlConnection conn = new SqlConnection(Connection.strCon))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader rd = command.ExecuteReader();
                    rd.Read();
                    if (rd.HasRows)
                    {
                        bool isAdmin = false;
                        //if (rd["��������������"].ToString() == "1") isAdmin = true;
                        View v = new View(isAdmin); // ����� ���������
                        v.ShowDialog();
                        tb_Login.Text = "";
                        tb_Password.Text = "";

                    }
                    else 
                    {
                        MessageBox.Show("�� ������ ������������");
                    }
                }
            }
            else
            {
                MessageBox.Show("�� ��� ���� ���������!");
            }
        }
    
        static void ���������������������������()
        {
            string query = $"select * from [Login].[������]";
            List<Product> list = new List<Product>();
            using (SqlConnection conn = new SqlConnection(Connection.strCon))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader rd = command.ExecuteReader();

                while (rd.Read())
                {
                    string strDescription = $"{rd["��������"].ToString()} \n " +
                                            $"�������������: {rd["�������������"].ToString()} \n " +
                                            $"����: {rd["���������"].ToString()}";
                    string path = null;
                    Image pict = null;
                    if (rd["�����������"].ToString() != "")
                    {
                        path = "C:\\Users\\giren\\OneDrive\\" +
                                "������� ����\\�����\\�����\\�����_import\\"
                                + rd["�����������"].ToString();
                        pict = (Image)(new Bitmap(path));
                    }
                    Product product = new Product(pict, strDescription, rd["���_��_��_������"].ToString());

                    list.Add(product);
                }
            }
            return list;
        }   

        static void ������������������������()
        {
            foreach (var prod in Product.GetListProduct())// ��� ��������� ��������� �����
            {
                flp_View.Controls.Add(prod);
            }
        }

        static void ���������������������()
        {
            
                var fio = textBox1.Text;
                var pol = comboBox1.Text;
                var email = textBox2.Text;
                var dr = dateTimePicker1.Text;
                var strana = textBox5.Text;
                var telephone = textBox3.Text;
                var napr = comboBox2.Text;
                var parol = textBox4.Text;

                string quertystring = $"insert into Lyudi (���, ���, �����, [���� ��������], ������, �������, �����������, ������) values ('{fio}', '{pol}','{email}','{dr}',{strana},'{telephone}','{napr}','{parol}')";


                using (SqlConnection conn = new SqlConnection(Connection.connection))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(quertystring, conn);
                        command.ExecuteNonQuery();
                        MessageBox.Show("������� ������� ������!", "�����!");
                    }
                    catch
                    {
                        MessageBox.Show("������� �� ������!");
                    }
                }
            

        }
        static string query = $"select ����������, ����_������, �����_������, h1.[���] as [����_1], " +
                $"h2.[���] as [����_2], h3.[���] as [����_3], h4.[���] as [����_4], " +
                $"h5.[���] as [����_5] from [dbo].[����] h1 " +
                $"join[dbo].[�����������] a on a.����_1 = h1.�����������" +
                $" join[dbo].[����] h2 on a.����_2 = h2.����������� " +
                $"join[dbo].[����] h3 on a.����_3 = h3.����������� " +
                $"join[dbo].[����] h4 on a.����_4 = h4.����������� " +
                $"join[dbo].[����] h5 on a.����_5 = h5.����������� " +
                $"where a.���������� = '{listBox1.SelectedItem}'";

        //��������� ������
        public static DataSet GetExecTasks(User user, string statusFilter)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string sql = "  select Task.Title, Task.[Status], [User].FirstName " +
                             "from[User], Executor, Task " +
                             "where Task.ExecutorID = Executor.ID " +
                             "and Executor.ID = [User].ID " +
                             "and Executor.ID = @id";
                if (!String.IsNullOrEmpty(statusFilter))
                    sql += " and Task.Status = @status";
                sql += " order by Task.CreateDateTime desc";
                SqlDataAdapter ada = new SqlDataAdapter(sql, conn);
                ada.SelectCommand.Parameters.AddWithValue("id", user.ID);
                ada.SelectCommand.Parameters.AddWithValue("status", statusFilter);
                ada.Fill(ds);
                return ds;
            }
        }

        static void ������������������()
        {
            // ��� ��������� ����
        }

        public static DataSet GetExecTasks(User user, string statusFilter)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string sql = "  select Task.Title, Task.[Status], [User].FirstName " +
                             "from[User], Executor, Task " +
                             "where Task.ExecutorID = Executor.ID " +
                             "and Executor.ID = [User].ID " +
                             "and Executor.ID = @id";
                if (!String.IsNullOrEmpty(statusFilter))
                    sql += " and Task.Status = @status";
                sql += " order by Task.CreateDateTime desc";
                SqlDataAdapter ada = new SqlDataAdapter(sql, conn);
                ada.SelectCommand.Parameters.AddWithValue("id", user.ID);
                ada.SelectCommand.Parameters.AddWithValue("status", statusFilter);
                ada.Fill(ds);
                return ds;
            }
        }

        private void Render()
        {
            TasksDataGrid.DataSource = DataWork.GetExecTasks(this.User, this.statusFilter).Tables[0];
        }



        static void ���������������������������()
        {
           
            // ��������� �������
            int _selectedClientID = int.Parse(dgClients.SelectedRows[0].Cells[0].Value.ToString());
            // �� � ������ �������� ����������� ������ �� ���� � ��������� ����� ��������

        }

        public static void ��������������(int ID)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                string sql = "delete from Client where ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("ID", ID);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedClientID == 0)
            {
                MessageBox.Show("�������� �������");
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete client data", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!DataWork.CanDeleteClient(_selectedClientID))
                {
                    MessageBox.Show("���������� ������� �������");
                    return;
                }
                DataWork.DeleteClient(_selectedClientID);
            }
        }
        public static DataSet ����������(string filter, string sort, string searchString, string searchCategory)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                DataSet ds = new DataSet();
                conn.Open();

                string sql = "������";

                if (!string.IsNullOrEmpty(filter))
                {
                    sql += $" WHERE Client.Gender = '{filter}'";
                }

                switch (searchCategory)
                {
                    case "���":
                        {
                            sql += string.IsNullOrEmpty(filter) ? " WHERE" : " AND";
                            sql += $" CONCAT(Client.LastName,' ', Client.FirstName,' ', Client.Patronymic) like '%{searchString}%'";
                            break;
                        }
                    case "Email":
                        {
                            sql += string.IsNullOrEmpty(filter) ? " WHERE" : " AND";
                            sql += $" Email like '%{searchString}%'";
                            break;
                        }
                    case "�������":
                        {
                            sql += string.IsNullOrEmpty(filter) ? " WHERE" : " AND";
                            sql += $" Phone like '%{searchString}%'";
                            break;
                        }
                }

                sql += " group by �";

                switch (sort)
                {
                    case "�������":
                        {
                            sql += " ORDER BY Client.LastName";
                            break;
                        }
                    case "���� ���������� ���������":
                        {
                            sql += " ORDER BY [��������� ���������] desc";
                            break;
                        }
                    case "���������� ���������":
                        {
                            sql += " ORDER BY [���������� ���������] desc";
                            break;
                        }
                    default:
                        {
                            sql += " ORDER BY Client.ID";
                            break;
                        }
                }

                SqlDataAdapter ada = new SqlDataAdapter(sql, conn);
                ada.Fill(ds);
                return ds;
            }
        }


    }
}
