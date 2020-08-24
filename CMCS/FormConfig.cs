using CMTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMCS
{
    public partial class FormConfig : Form
    {
        CMC form;
        public FormConfig()
        {
            InitializeComponent();
        }
        public FormConfig(CMC f)
        {
            InitializeComponent();
            form = f;
            Iniciar();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool b = false;
            CMConfig.MODELS = chkModels.Checked;
            if (CMConfig.ValidateNamespace(txtClases.Text))
            {
                CMConfig.CUSTOMNAMESPACE = txtClases.Text;
            }
            else
            {
                b = true;
            }
            if (CMConfig.ValidateNamespace(txtJava.Text))
            {
                CMConfig.JAVANAMESPACE = txtJava.Text;
            }
            else
            {
                b = true;
            }
            if (CMConfig.ValidateNamespace(txtCS.Text))
            {
                CMConfig.CSNAMESPACE = txtCS.Text;
            }
            else
            {
                b = true;
            }
            if (b)
            {
                MessageBox.Show("Namespace/Package ingresado esta reservado por el sistema operativo", "Error al ingresar uno o mas Namespace(s)/Package(s)", MessageBoxButtons.OK,MessageBoxIcon.Error);
                Iniciar();
                return;
            }
            Dispose();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        ~FormConfig()
        {
            form.Focus();
        }
        public void Iniciar()
        {
            chkModels.Checked = CMConfig.MODELS;
            txtClases.Text = CMConfig.CUSTOMNAMESPACE;
            txtCS.Text = CMConfig.CSNAMESPACE;
            txtJava.Text = CMConfig.JAVANAMESPACE;
        }
    }
}
