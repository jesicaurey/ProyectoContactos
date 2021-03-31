using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoContactos
{
    public class CapaLogicaNegocio
    {
        private CapaAccesoDatos _capaAccesoDatos;

        public CapaLogicaNegocio()
        {
            _capaAccesoDatos = new CapaAccesoDatos();
        }
        public Agenda GuardarContacto(Agenda agenda)
        {
            if (agenda.Id == 0)
                _capaAccesoDatos.InsertContacto(agenda);
            else
                _capaAccesoDatos.UpdateContacto(agenda);

            return agenda;
        }

        public List<Agenda> GetAgendas(string buscarText = null)
        {
            return _capaAccesoDatos.GetAgendas(buscarText);
        }

        public void EliminarContacto(int Id)
        {
            _capaAccesoDatos.EliminarContacto(Id);
        }
    }
}
