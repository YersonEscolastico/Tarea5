using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tarea5.Utilitarios;

namespace Tarea5.Consultas
{
    public partial class cEvaluaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static List<Evaluaciones> Buscar(int index, string criterio, DateTime desde, DateTime hasta)
        {
            Expression<Func<Evaluaciones, bool>> filtro = p => true;
            RepositorioBase<Evaluaciones> repositorio = new RepositorioBase<Evaluaciones>();
            List<Evaluaciones> list = new List<Evaluaciones>();

            int id = Utils.ToInt(criterio);
            switch (index)
            {
                case 0://Todo
                    repositorio.GetList(c => true);
                    break;
                case 1://Id
                    filtro = p => p.EvaluacionId == id && p.Fecha >= desde && p.Fecha <= hasta;
                    break;
                case 2://Id
                    filtro = p => p.EstudianteId == id && p.Fecha >= desde && p.Fecha <= hasta;
                    break;
                case 3://Nombres
                    filtro = p => p.TotalPerdido == id && p.Fecha >= desde && p.Fecha <= hasta;
                    break;
                case 4://Todo por fecha
                    filtro = p => p.Fecha >= desde && p.Fecha <= hasta;
                    break;
            }

            list = repositorio.GetList(filtro);

            return list;
        }


        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            int id = Utils.ToInt(CriterioTextBox.Text);
            int index = FiltroDropDownList.SelectedIndex;
            DateTime desde = Utils.ToDateTime(DesdeTextBox.Text);
            DateTime hasta = Utils.ToDateTime(HastaTextBox.Text);

            DatosGridView.DataSource = Buscar(index, CriterioTextBox.Text, desde, hasta);
            DatosGridView.DataBind();
        }

        public static List<Estudiantes> Lista(Expression<Func<Estudiantes, bool>> Filtro)
        {
            Filtro = r => true;
            RepositorioBase<Estudiantes> Repositorio = new RepositorioBase<Estudiantes>();
            List<Estudiantes> Estudiantes = new List<Estudiantes>();
            Estudiantes = Repositorio.GetList(Filtro);
            return Estudiantes;
        }
    }
}