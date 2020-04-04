using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Question1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            DataTable dt = DAO.GetDataTable("SELECT member_no, firstname, lastname, region_name" +
                " FROM member m, region r" +
                " WHERE m.region_no = r.region_no");
            int a =1;
            dataGridView1.DataSource = dt;
            dt.Columns[0].ColumnName = "Member ID";
            dt.Columns[3].ColumnName = "Region name";

            DataGridViewCheckBoxColumn dgvCmb = new DataGridViewCheckBoxColumn();
            dgvCmb.ValueType = typeof(bool);
            dgvCmb.Name = "Chk";
            dgvCmb.HeaderText = "Select";
            dataGridView1.Columns.Add(dgvCmb);
            dataGridView1.Columns["chk"].DisplayIndex = 0;

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.Name = "button";
            button.HeaderText = "Edit";
            button.Text = "Edit";
            button.UseColumnTextForButtonValue = true; //dont forget this line
            this.dataGridView1.Columns.Add(button);
            dataGridView1.Columns["button"].DisplayIndex = dataGridView1.ColumnCount - 1;

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = 0;
            for(int i=0; i< dataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow r = dataGridView1.Rows[i];
                    if(r.Cells[0].Value != null)
                    if (((bool)r.Cells[0].Value) == true)
                            {
                                MessageBox.Show("Member_no = " + r.Cells[2].Value.ToString());
                                n++;
                               
                                SqlCommand cmd = new SqlCommand("delete from member where member_no = @n");
                                cmd.Parameters.AddWithValue("@n", (int)r.Cells[2].Value);
                                DAO.UpdateTable(cmd);

                                dataGridView1.Rows.Remove(r);
                                i--;
                            }
                
                }

           /* DataTable dt = DAO.GetDataTable("SELECT member_no, firstname, lastname, region_name" +
                " FROM member m, region r" +
                " WHERE m.region_no = r.region_no");

            dataGridView1.DataSource = dt;
            dt.Columns[0].ColumnName = "Member ID";
            dt.Columns[3].ColumnName = "Region name";

            dataGridView1.Columns[2].DisplayIndex = 1;
            dataGridView1.Columns[3].DisplayIndex = 2;
            dataGridView1.Columns[4].DisplayIndex = 3;
            dataGridView1.Columns[5].DisplayIndex = 4;

            dataGridView1.Columns["chk"].DisplayIndex = 0;
            dataGridView1.Columns["button"].DisplayIndex = dataGridView1.ColumnCount - 1;*/

            MessageBox.Show(string.Format("Deleted {0} members", n));
                       
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                frmEdit f = new frmEdit(int.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()));
                if (f.ShowDialog() == DialogResult.OK)
                {
                    DataTable dt = DAO.GetDataTable("SELECT member_no, firstname, lastname, region_name" +
                            " FROM member m, region r" +
                            " WHERE m.region_no = r.region_no");

                    dt.Columns[0].ColumnName = "Member ID";
                    dt.Columns[3].ColumnName = "Region name";
                    dataGridView1.DataSource = dt;

                    dataGridView1.Columns["chk"].DisplayIndex = 0;
                    dataGridView1.Columns["button"].DisplayIndex = dataGridView1.ColumnCount - 1;
                       
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
