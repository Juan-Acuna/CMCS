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
            chkModels.Checked = CMConfig.MODELS;
            txtClases.Text = CMConfig.CUSTOMNAMESPACE;
            txtCS.Text = CMConfig.CSNAMESPACE;
            txtJava.Text = CMConfig.JAVANAMESPACE;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            CMConfig.MODELS = chkModels.Checked;
            CMConfig.CUSTOMNAMESPACE = txtClases.Text;
            CMConfig.JAVANAMESPACE = txtJava.Text;
            CMConfig.CSNAMESPACE = txtCS.Text;
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
    }
}
