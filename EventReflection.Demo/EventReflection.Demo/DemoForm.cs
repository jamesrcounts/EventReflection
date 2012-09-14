namespace EventReflection.Demo
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class DemoForm : Form
    {
        private Button button1;

        private CheckBox checkBox1;

        private ListBox listBox1;

        public DemoForm()
        {
            this.button1 = new Button();
            this.button1.Text = "Click Me!";
            this.Controls.Add(this.button1);

            this.checkBox1 = new CheckBox();
            this.checkBox1.Text = "Check Me!";
            this.checkBox1.Location = new Point(
                this.button1.Location.X + this.button1.Width + 10,
                this.button1.Location.Y);
            this.Controls.Add(this.checkBox1);

            this.listBox1 = new ListBox();
            this.listBox1.Location = new Point(
                10,
                this.button1.Location.Y + this.button1.Height + 10);
            this.listBox1.Size = new Size(
                this.Width - 40,
                this.Height - this.button1.Height - 40);
            this.Controls.Add(this.listBox1);

            this.button1.Click += this.ButtonClick;
            this.checkBox1.CheckedChanged += this.HandleCheckedChanged;
            this.Load += this.HandleFormLoad;
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            return;
        }

        private void HandleCheckedChanged(object sender, EventArgs e)
        {
            return;
        }

        private void HandleFormLoad(object sender, EventArgs e)
        {
            return;
        }
    }
}