using McExample.BLL;
using McExample.BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace McExample.WinForms
{
    public partial class FrmProductEdit : Form
    {
        private Action callBack;
        public FrmProductEdit()
        {
            InitializeComponent();
        }      

        public FrmProductEdit(Action callBack):this()
        {
            this.callBack = callBack;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkForm();
                Product product = new Product
                (
                txtReference.Text,
                txtName.Text,
                double.Parse(txtPrice.Text),
                float.Parse(txtTaxe.Text)
                );
                ProductBLO productBLO = new ProductBLO(ConfigurationManager.AppSettings["DbFolder"]);
                productBLO.CreateProduct(product);

                MessageBox.Show
                (
                    "Save done!",
                    "Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                 );

                if (callBack != null)
                    callBack();
                txtReference.Clear();
                txtName.Clear();
                txtPrice.Clear();
                txtTaxe.Clear();
                txtReference.Focus();
            }

            catch (TypingException ex)
            {
                MessageBox.Show
                (
                    "Une erreur est survenue, veuillez reessayez plus tard SVP",
                    "Erreur de saisie",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            catch (DuplicateNameException ex)
            {
                ex.WriteToFile();
                MessageBox.Show
                (
                    ex.Message,
                    "Erreur, déjà existant",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            catch (Exception ex)
            {
                ex.WriteToFile();
                MessageBox.Show
                (
                    ex.Message,
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }                       
        }

        private void checkForm()
        {
            string text = string.Empty;
            txtReference.BackColor = Color.White;
            txtName.BackColor = Color.White;
            if (string.IsNullOrWhiteSpace(txtReference.Text))
            {
                text += "- Please, entrer une bonne référence !\n";
                txtReference.BackColor = Color.Aqua;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                text += "- Please, entrer le nom !\n";
                txtName.BackColor = Color.Red;
            }

            if (!string.IsNullOrEmpty(text))
                throw new TypingException(text);
        }

        private void FrmProductEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
