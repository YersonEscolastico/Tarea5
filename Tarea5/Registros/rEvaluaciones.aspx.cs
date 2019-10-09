using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tarea5.Utilitarios;

namespace Tarea5.Registros
{
    public partial class rEvaluaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                LLenarEstudiantes();
                LlenarCategorias();
                EvaluacionIdTextBox.Text = "0";
                EstudianteDropDownList.Text = string.Empty;
                ValorTextBox.Text = "0";
                LogradoTextBox.Text = "0";
                FechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

                ViewState["Evaluaciones"] = new Evaluaciones();
                ViewState["Detalle"] = new Evaluaciones().Detalle;
                BindGrid();
            }

        }

        private void Limpiar()
        {
            EvaluacionIdTextBox.Text = 0.ToString();
            FechaTextBox.Text = DateTime.Now.ToString();
            ValorTextBox.Text = 0.ToString();
            LogradoTextBox.Text = 0.ToString();
            TotalTextBox.Text = 0.ToString();
            base.ViewState["Evaluaciones"] = new Evaluaciones();
            DetalleGridView.DataSource = null;
            DetalleGridView.DataBind();
            BindGrid();
        }

        public Evaluaciones LlenaClase()
        {
            Evaluaciones evaluacion = new Evaluaciones();
            evaluacion = (Evaluaciones)ViewState["Evaluaciones"];

            evaluacion.EvaluacionId = Convert.ToInt32(EvaluacionIdTextBox.Text);
            evaluacion.EstudianteId = Convert.ToInt32(EstudianteDropDownList.Text);
            evaluacion.Fecha = Convert.ToDateTime(FechaTextBox.Text);
            evaluacion.TotalPerdido = TotalTextBox.Text.ToDecimal();
            return evaluacion;
        }


        private void LlenaCampo(Evaluaciones evaluacion)
        {

            ((Evaluaciones)ViewState["Evaluaciones"]).Detalle = evaluacion.Detalle;
            EvaluacionIdTextBox.Text = evaluacion.EvaluacionId.ToString();
            EstudianteDropDownList.Text = evaluacion.EstudianteId.ToString();
            FechaTextBox.Text = evaluacion.Fecha.ToString();
            TotalTextBox.Text = evaluacion.TotalPerdido.ToString();
            this.BindGrid();
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Evaluaciones> db = new RepositorioBase<Evaluaciones>();
            Evaluaciones evaluacion = db.Buscar(Convert.ToInt32(EvaluacionIdTextBox.Text));
            return (evaluacion != null);

        }


        protected void BindGrid()
        {
            if (ViewState["Evaluaciones"] != null)
            {
                DetalleGridView.DataSource = ((Evaluaciones)ViewState["Evaluaciones"]).Detalle;
                DetalleGridView.DataBind();
            }
        }


        protected void AgregarButton_Click1(object sender, EventArgs e)
        {
            Evaluaciones evaluacion = new Evaluaciones();
            EvaluacionesDetalle evaluaciones = new EvaluacionesDetalle();

            evaluacion = (Evaluaciones)ViewState["Evaluaciones"];
            decimal perdidos = 0;
            perdidos = Utils.ToDecimal(ValorTextBox.Text) - Utils.ToDecimal(LogradoTextBox.Text);

            evaluacion.AgregarDetalle(0, Convert.ToInt32(EvaluacionIdTextBox.Text), CategoriaDropDownList.Text, Convert.ToDecimal(ValorTextBox.Text), Convert.ToDecimal(LogradoTextBox.Text), perdidos);

            ViewState["Evaluaciones"] = evaluacion;

            this.BindGrid();

            decimal Calculador = 0;
            foreach (var item in evaluacion.Detalle)
            {
                Calculador = Calculador + item.Perdido;
            }
            TotalTextBox.Text = Calculador.ToString();

            ValorTextBox.Text = "0";
            LogradoTextBox.Text = "0";
        }


        private void LLenarEstudiantes()
        {
            if (!Page.IsPostBack)
            {
                RepositorioBase<Estudiantes> db = new RepositorioBase<Estudiantes>();


                EstudianteDropDownList.DataSource = db.GetList(t => true);
                EstudianteDropDownList.DataValueField = "EstudianteId";
                EstudianteDropDownList.DataTextField = "Nombres";
                EstudianteDropDownList.DataBind();

                ViewState["Evaluacion"] = new Evaluaciones();
            }

        }
        private bool Validar()
        {
            bool paso = false;
            if (String.IsNullOrEmpty(EvaluacionIdTextBox.Text))
            {
                Response.Write("<script>alert('Debe ingresar un id');</script>");
                paso = true;
            }
            if (String.IsNullOrEmpty(CategoriaDropDownList.Text))
            {
                Response.Write("<script>alert('Debe ingresar una cantidad');</script>");
                paso = true;
            }


            if (String.IsNullOrEmpty(ValorTextBox.Text))
            {
                Response.Write("<script>alert('Debe ingresar un valor');</script>");
                paso = true;
            }
            if (String.IsNullOrEmpty(LogradoTextBox.Text))
            {
                Response.Write("<script>alert('Debe ingresar un valor');</script>");
                paso = true;
            }

            return paso;
        }
        protected void GuardarButton_Click1(object sender, EventArgs e)
        {
            bool paso = false;
            Evaluaciones evaluacion = LlenaClase();
            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();

            if (Validar())
                return;
            else

         if (evaluacion.EvaluacionId == 0)
                paso = repositorio.Guardar(evaluacion);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    return;
                }
                paso = repositorio.Modificar(evaluacion);
            }

            if (paso)
            {
                Utils.ShowToastr(this.Page, "Guardao!!", "Guardado", "info");
                Limpiar();
                return;
            }
            else
            {
                Utils.ShowToastr(this.Page, "Error!!", "error");
            }
        }

        protected void NuevoButton_Click1(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void EliminarButton_Click1(object sender, EventArgs e)
        {

            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();
            int id = EvaluacionIdTextBox.Text.ToInt();

            if (repositorio.Eliminar(Utils.ToInt(EvaluacionIdTextBox.Text)))
            {
                Utils.ShowToastr(this.Page, "Eliminado!!", "Eliminado");
                Limpiar();
            }
            else
                Utils.ShowToastr(this.Page, "Error!!", "error");
        }


        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioEvaluacion repositorio = new RepositorioEvaluacion();
            Evaluaciones evaluaciones = repositorio.Buscar(EvaluacionIdTextBox.Text.ToInt());
            if (evaluaciones != null)
            {
                Limpiar();
                LlenaCampo(evaluaciones);
            }
        }


        private void LlenarCategorias()
        {
            if (!Page.IsPostBack)
            {
                RepositorioBase<Categorias> db = new RepositorioBase<Categorias>();


                CategoriaDropDownList.DataSource = db.GetList(t => true);
                CategoriaDropDownList.DataValueField = "CategoriaId";
                CategoriaDropDownList.DataTextField = "Descripcion";
                CategoriaDropDownList.DataBind();

                ViewState["Evaluacion"] = new Evaluaciones();
            }

        }

        protected void Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Evaluaciones evaluaciones = new Evaluaciones();

            evaluaciones = (Evaluaciones)ViewState["Evaluaciones"];

            ViewState["Detalle"] = evaluaciones.Detalle;

            int Fila = e.RowIndex;

            evaluaciones.Detalle.RemoveAt(Fila);

            this.BindGrid();

            foreach (var item in evaluaciones.Detalle)
            {
                TotalTextBox.Text = item.Perdido.ToString();
            }


            decimal Calculador = 0;
            foreach (var item in evaluaciones.Detalle)
            {
                Calculador = Calculador + item.Perdido;
            }
            TotalTextBox.Text = Calculador.ToString();

            ValorTextBox.Text = "0";
            LogradoTextBox.Text = "0";
        }
    }
}