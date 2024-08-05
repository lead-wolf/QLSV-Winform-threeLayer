using DAL.Entity;
using LAB_5._0.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB_5._0.GUI
{
    public partial class Form1 : Form
    {
        //private readonly StudentService studentService = new StudentService();
        private readonly StudentService studentService = StudentService.Instance;
        private readonly FacultyService facultyService = FacultyService.Instance;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                setGridViewStyle(dataGridView1);
                var listFacultys = facultyService.GetAll();
                var listStudents = studentService.GetAll();
                FillFalcultyCombobox(listFacultys);
                BindGrid(listStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        //Hàm binding list dữ liệu khoa vào combobox có tên hiện thị là tên khoa, giá trị là Mã khoa         
        private void FillFalcultyCombobox(List<Faculty> listFacultys)
        {
            listFacultys.Insert(0, new Faculty());
            this.cmbKhoa.DataSource = listFacultys;
            this.cmbKhoa.DisplayMember = "FacultyName";
            this.cmbKhoa.ValueMember = "FacultyID";
        }

        //Hàm binding gridView từ list sinh viên                  
        private void BindGrid(List<Student> listStudent)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.StudentID;
                dataGridView1.Rows[index].Cells[1].Value = item.FullName;

                if (item.Faculty != null)
                    dataGridView1.Rows[index].Cells[2].Value = item.Faculty.FacultyName;

                dataGridView1.Rows[index].Cells[3].Value = item.AverageScore + "";

                if (item.MajorID != null)
                    dataGridView1.Rows[index].Cells[4].Value = item.Major.MajorName + "";

                    ShowAvatar(item.avater);
                  
                    
            }
        }
        private void ShowAvatar(string ImageName)
        {
            if (string.IsNullOrEmpty(ImageName))
            {
                picAvatar.Image = null;
            }
            else
            {
                string parentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                string imagePath = Path.Combine(parentDirectory, "Images", ImageName);
                picAvatar.Image = Image.FromFile(imagePath);
                picAvatar.Refresh();
            }
        }

        public void setGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnThemSua_Click(object sender, EventArgs e)
        {
            if (checkDataInput())
            {  
                Student s = studentService.FindById(txtMssv.Text);

                if (s == null)
                {
                    // Thêm mới sinh viên
                    s = new Student();
                    s.StudentID = txtMssv.Text;
                    s.FullName = txtHoTen.Text;
                    s.AverageScore = double.Parse(txtDiem.Text);
                    s.FacultyID = (int)cmbKhoa.SelectedValue;
                    //s.Faculty = facultyService.FindByID((int)cmbKhoa.SelectedValue);
                    //s.avater
                    studentService.InsertUpdate(s);
                    BindGrid(studentService.GetAll());
                    MessageBox.Show("Thêm dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }else
                {
                    s = new Student();
                    s.StudentID = txtMssv.Text;
                    s.FullName = txtHoTen.Text;
                    s.AverageScore = double.Parse(txtDiem.Text);
                    s.FacultyID = (int)cmbKhoa.SelectedValue;
                    studentService.InsertUpdate(s);
                    BindGrid(studentService.GetAll());
                    MessageBox.Show("Cập nhật dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }        
                
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            txtMssv.Text = row.Cells["MSSV"].Value.ToString();
            txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
            txtDiem.Text = row.Cells["DiemTb"].Value.ToString();
            cmbKhoa.Text = row.Cells["Khoa"].Value.ToString();
            ShowAvatar(studentService.FindById(txtMssv.Text).avater);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(checkDataInput())
            {
                Student dbDelete = studentService.FindById(txtMssv.Text);
                if (dbDelete != null)
                {
                    if (MessageBox.Show("Xác nhận xóa", "thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        //studentService.Remove(dbDelete);
                        studentService.RemoveByID(dbDelete.StudentID);
                    }
                }

                BindGrid(studentService.GetAll());
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //developer define
        private bool checkDataInput()
        {
            if (string.IsNullOrEmpty(txtMssv.Text.ToString()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else

            if (string.IsNullOrEmpty(txtHoTen.Text.ToString()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else

            if (string.IsNullOrEmpty(txtDiem.Text.ToString()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else

            if ((txtMssv.Text.ToString().Length != 10))
            {
                MessageBox.Show("Mã số sinh viên phải có 10 kí tự số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else

            if (!float.TryParse(txtDiem.Text, out float temple))
            {
                MessageBox.Show("Điểm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            return true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var listStudents = new List<Student>();
            if (this.checkBox1.Checked)
                listStudents = studentService.GetAllHasNoMajor();
            else
                listStudents = studentService.GetAll();
            BindGrid(listStudents);

        }
    }
}
