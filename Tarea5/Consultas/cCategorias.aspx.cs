using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tarea5.Utilitarios;

namespace Tarea5.Consultas
{
    public partial class cCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static List<Categorias> Buscar(int index, string criterio, DateTime desde, DateTime hasta)
        {
            Expression<Func<Categorias, bool>> filtro = p => true;
            RepositorioBase<Categorias> repositorio = new RepositorioBase<Categorias>();
            List<Categorias> list = new List<Categorias>();

            int id = Utils.ToInt(criterio);
            switch (index)
            {
                case 0://Todo
                    repositorio.GetList(c => true);
                    break;
                case 1://Id
                    filtro = p => p.CategoriaId == id && p.Fecha >= desde && p.Fecha <= hasta;
                    break;
                case 2://Nombres
                    filtro = p => p.Descripcion.Contains(criterio) && p.Fecha >= desde && p.Fecha <= hasta;
                    break;
                case 3://Todo por fecha
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

        public static List<Categorias> Lista(Expression<Func<Categorias, bool>> Filtro)
        {
            Filtro = r => true;
            RepositorioBase<Categorias> Repositorio = new RepositorioBase<Categorias>();
            List<Categorias> Categorias = new List<Categorias>();
            Categorias = Repositorio.GetList(Filtro);
            return Categorias;
        }
    }
}