using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Minesweeper {
    public partial class CustomFieldDialog : Form {
        public new int Width {
            get {
                return ToInt(widthBox.Text);
            }
            set {
                widthBox.Text = value.ToString();
            }
        }

        public new int Height {
            get {
                return ToInt(heightBox.Text);
            }
            set {
                heightBox.Text = value.ToString();
            }
        }

        public int Mines {
            get {
                return ToInt(minesBox.Text);
            }
            set {
                minesBox.Text = value.ToString();
            }
        }

        private int ToInt(string str) {
            int i;
            if (int.TryParse(RemoveNonNumbers(str), out i)) {
                return i;
            }
            return 1;
        }

        private string RemoveNonNumbers(string str) {
            return Regex.Replace(str, "[^0-9]", "");
        }

        private void ValidateWidthBox(object sender, EventArgs e) {
            widthBox.Text = RemoveNonNumbers(widthBox.Text);
        }

        private void ValidateHeightBox(object sender, EventArgs e) {
            heightBox.Text = RemoveNonNumbers(heightBox.Text);
        }

        private void ValidateMinesBox(object sender, EventArgs e) {
            minesBox.Text = RemoveNonNumbers(minesBox.Text);
        }

        public CustomFieldDialog() {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
