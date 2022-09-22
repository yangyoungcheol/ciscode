using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace ciscode
{
    public partial class Form2 : Form
    {

        static String mysql_str = "server=127.0.0.1;port=3306;Database=yyc;Uid=root;Pwd=1234;Charset=utf8";
        MySqlConnection conn = new MySqlConnection(mysql_str);
        MySqlCommand cmd;  //sql문장을 실행시킬때
        MySqlDataReader reader;   //sql문장을 실행시키고 결과받을때
        private bool Select_sw = false;
        private bool select_cgsw = false;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //String co = conn.State.ToString();
            //MessageBox.Show(co);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                display_code();
                //co = conn.State.ToString();
                //MessageBox.Show(co);
            }
        }
        private void display_code()
        {

            // sql문 작성
            // 연결된
            dataGridView1.Rows.Clear();
            String sql = "select * from yyc_cdg";

            if (reader != null) reader.Close();
            cmd = new MySqlCommand();  //cmd sql위한 준비작업
            cmd.Connection = conn;
            cmd.CommandText = sql;   //실행시킬 sql문장이 무엇인지 지정
            //cmd.Prepare();
            //cmd.Parameters.AddWithValue("@name1", textBox1.Text + "%");
            //@number가 어떤 textbox값인지 알려줌
            reader = cmd.ExecuteReader();

            while (reader.Read() == true)
            {
                //read해서 data가 읽히면 계속 작업
                comboBox1.Items.Add((String)reader["cdg_grpnm"]);
                comboBox2.Items.Add((string)reader["cdg_grpcd"]);
                comboBox3.Items.Add((int)reader["cdg_digit"]);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            // sql문 작성
            // 연결된
            dataGridView1.Rows.Clear();
            String sql = "select  unit_grpcd, unit_cd, unit_nm, unit_nm2, unit_seq, unit_use from yyc_cdg " +
                         " Where unit_grpcd=@unit_grpcd";


            if (reader != null) reader.Close();
            cmd = new MySqlCommand();  //cmd sql위한 준비작업
            cmd.Connection = conn;
            cmd.CommandText = sql;   //실행시킬 sql문장이 무엇인지 지정
            // cmd.Prepare();
            cmd.Parameters.AddWithValue("@unit_grpcd", comboBox2.Text);
            //cmd.Prepare();
            //cmd.Parameters.AddWithValue("@name1", textBox1.Text + "%");
            //@number가 어떤 textbox값인지 알려줌
            reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read() == true)
            {
                //read해서 data가 읽히면 계속 작업
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[1].Value = (string)reader["unit_cd"];
                dataGridView1.Rows[i].Cells[2].Value = (string)reader["unit_nm"];
                dataGridView1.Rows[i].Cells[3].Value = (int)reader["unit_nm2"];
                dataGridView1.Rows[i].Cells[4].Value = (int)reader["unit_seq"];
                dataGridView1.Rows[i].Cells[5].Value = (string)reader["unit_use"];
                dataGridView1.Rows[i].Cells[6].Value = comboBox2.Text;
                dataGridView1.Rows[i].Cells[7].Value = comboBox3.Text;
                // dataGridView1.Rows[i].Cells[5].Value = (string)reader["cdg_kind"];
                i++;

            }
            if (i == 0)
            {
                MessageBox.Show("조회될 data가 없습니다.");
            }
            // this.dataGridView1_SelectionChanged(null, null);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            comboBox2.SelectedIndex = i;
            comboBox3.SelectedIndex = i;
        }

    }

}
