using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementSystem
{
    public partial class AddCoworkerForm : Form
    {
        public int accountId;
        public int projectId;

        public AddCoworkerForm(int accountId, int projectId)
        {
            InitializeComponent();
            this.accountId = accountId;
            this.projectId = projectId;
            comboBox1.DataSource = new List<Color>{Color.FromName("Blue"), Color.FromName("Green"), Color.FromName("Orange"), Color.FromName("Purple"), Color.FromName("Pink"), Color.FromName("Brown"), Color.FromName("Teal"), Color.FromName("Cyan"), Color.FromName("Magenta"), Color.FromName("Gold"), Color.FromName("Indigo"), Color.FromName("Maroon"), Color.FromName("Turquoise")};
            comboBox1.MaxDropDownItems = 10;
            comboBox1.IntegralHeight = false;
            comboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DrawItem += comboBox1_DrawItem;
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index >= 0)
            {
                var txt = comboBox1.GetItemText(comboBox1.Items[e.Index]);
                var color = (Color)comboBox1.Items[e.Index];
                var r1 = new Rectangle(e.Bounds.Left + 1, e.Bounds.Top + 1,
                    2 * (e.Bounds.Height - 2), e.Bounds.Height - 2);
                var r2 = Rectangle.FromLTRB(r1.Right + 2, e.Bounds.Top,
                    e.Bounds.Right, e.Bounds.Bottom);
                using (var b = new SolidBrush(color))
                    e.Graphics.FillRectangle(b, r1);
                e.Graphics.DrawRectangle(Pens.Black, r1);
                TextRenderer.DrawText(e.Graphics, txt, comboBox1.Font, r2,
                    comboBox1.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Account account = new Account(textPassword.Text, 1, textFirstName.Text, textLastName.Text, textEmail.Text, this.projectId);
            Coworker coworker = new Coworker(account.accountId, account.status, account.firstName, account.lastName, (Color)comboBox1.SelectedValue, this.projectId);
            new ManagerDashBoardForm(this.accountId, projectId).Show();
            this.Hide();
        }

        private void AddCoworkerForm_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
