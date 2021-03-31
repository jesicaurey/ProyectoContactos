using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoContactos
{
    public partial class DatosContacto : Form
    {
        private CapaLogicaNegocio _capaLogicaNegocio;
        private Agenda _agenda;
        public DatosContacto()
        {
            InitializeComponent();
            _capaLogicaNegocio = new CapaLogicaNegocio();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarContacto();
            this.Close();
            ((Menu)this.Owner).PopulateContactos();
        }

        private void GuardarContacto()
        {
            Agenda agenda = new Agenda();
            agenda.Nombre = txtNombre.Text;
            agenda.Apellido = txtApellido.Text;
            agenda.Telefono = txtTelefono.Text;
            agenda.Direccion = txtDireccion.Text;

            agenda.Id = _agenda != null ? _agenda.Id : 0;

            _capaLogicaNegocio.GuardarContacto(agenda);
        }

        public void LoadContacto(Agenda agenda)
        {
            _agenda = agenda;
            if(agenda != null)
            {
                ClearForm();

                txtNombre.Text = agenda.Nombre;
                txtApellido.Text = agenda.Apellido;
                txtTelefono.Text = agenda.Telefono;
                txtDireccion.Text = agenda.Direccion;
            }
        }

        private void ClearForm()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty;
        }

        private void DatosContacto_Load(object sender, EventArgs e)
        {

        }
    }
}
