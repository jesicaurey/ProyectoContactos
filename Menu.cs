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
    public partial class Menu : Form
    {

        private CapaLogicaNegocio _capaLogicaNegocio;
        public Menu()
        {
            InitializeComponent();
            _capaLogicaNegocio = new CapaLogicaNegocio();
        }



        private void btnAgregar_Click(object sender, EventArgs e)
        {
            OpenDatosContactoDialog();
        }

        private void OpenDatosContactoDialog()
        {
            DatosContacto datosContacto = new DatosContacto();
            datosContacto.ShowDialog(this);
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            PopulateContactos();
        }

        public void PopulateContactos(string buscarText = null)
        {
            List<Agenda> agendas = _capaLogicaNegocio.GetAgendas(buscarText);
            gridContactos.DataSource = agendas;
        }

        private void gridContactos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)gridContactos.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if(cell.Value.ToString() == "Editar")
            {
                DatosContacto datosContacto = new DatosContacto();
                datosContacto.LoadContacto(new Agenda
                {
                    Id = int.Parse(gridContactos.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    Nombre = gridContactos.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    Apellido = gridContactos.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Telefono = gridContactos.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Direccion = gridContactos.Rows[e.RowIndex].Cells[4].Value.ToString(),
                });
                datosContacto.ShowDialog(this);
            }
            else if(cell.Value.ToString() == "Eliminar")
            {
                EliminarContacto(int.Parse(gridContactos.Rows[e.RowIndex].Cells[0].Value.ToString()));
                PopulateContactos();
            }

        }
        private void EliminarContacto(int Id)
        {
            _capaLogicaNegocio.EliminarContacto(Id);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            PopulateContactos(txtBuscar.Text);
            txtBuscar.Text = string.Empty;
        }
    }
}
