using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarea5.Registros
{
    public partial class rCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            PromedioPTextBox.Text = "0";
            if (!Page.IsPostBack)
            {
                fechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                

                int ID = Utilitarios.Utils.ToInt(Request.QueryString["id"]);

                if (ID > 0)
                {
                    RepositorioBase<Categorias> repositorio = new RepositorioBase<Categorias>();
                    var us = repositorio.Buscar(ID);

                    if (us == null)
                    {
                        Utilitarios.Utils.ShowToastr(this.Page, "Registro No encontrado", "Error", "error");
                    }
                    else
                    {
                        LlenaCampo(us);
                    }
                }
            }

        }

        private void Limpiar()
        {
            CategoriaIdTextBox.Text = string.Empty;
            DescripcionTextBox.Text = string.Empty;
            PromedioPTextBox.Text = 0.ToString();
            fechaTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");

        }

        public Categorias LlenaClase()
        {
            Categorias caregoria = new Categorias();
            caregoria.CategoriaId = Convert.ToInt32(CategoriaIdTextBox.Text);
            caregoria.Descripcion = DescripcionTextBox.Text;
            caregoria.Fecha = Convert.ToDateTime(fechaTextBox.Text);
            caregoria.PromedioPerdido = Convert.ToDecimal(PromedioPTextBox.Text);

            return caregoria;
        }


        private void LlenaCampo(Categorias categoria)
        {
            CategoriaIdTextBox.Text = categoria.CategoriaId.ToString();
            DescripcionTextBox.Text = categoria.Descripcion;
            PromedioPTextBox.Text = categoria.PromedioPerdido.ToString();
            fechaTextBox.Text = categoria.Fecha.ToString("yyyy-MM-dd");
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Categorias> db = new RepositorioBase<Categorias>();
            Categorias ca = db.Buscar(Convert.ToInt32(CategoriaIdTextBox.Text));
            return (ca != null);

        }



        private bool ValidarCampos()
        {
            bool paso = true;
            if (DescripcionTextBox.Text == string.Empty)
            {
                Utilitarios.Utils.ShowToastr(this.Page, "El campo descripcion no puede estar vacio", "Error", "error");

                DescripcionTextBox.Focus();
                paso = false;
            }


            return paso;
        }


        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            int id;

            RepositorioBase<Categorias> db = new RepositorioBase<Categorias>();
            Categorias categoria = new Categorias();
            int.TryParse(CategoriaIdTextBox.Text, out id);
            Limpiar();

            categoria = db.Buscar(id);

            if (categoria != null)
            {

                LlenaCampo(categoria);

            }
            else
            {
                Utilitarios.Utils.ShowToastr(this.Page, "No se encontro el Estudiante", "Error", "error");

            }
        }

        protected void NuevoButton_Click1(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Categorias> db = new RepositorioBase<Categorias>();
            Categorias categoria;
            bool paso = false;


            if (!ValidarCampos())
                return;

            categoria = LlenaClase();


            if (CategoriaIdTextBox.Text == Convert.ToString(0))
            {
                paso = db.Guardar(categoria);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    Utilitarios.Utils.ShowToastr(this.Page, "Este registro no existe", "Error", "error");
                    return;
                }
                paso = db.Modificar(categoria);
            }

            if (paso)
                Utilitarios.Utils.ShowToastr(this.Page, "Ha guardado Correctamente", "Exito", "success");
            else
                Utilitarios.Utils.ShowToastr(this.Page, "Se profujo un error al guardar", "Error", "error");
            Limpiar();

        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            if (Utilitarios.Utils.ToInt(CategoriaIdTextBox.Text) > 0)
            {
                int id = Convert.ToInt32(CategoriaIdTextBox.Text);
                RepositorioBase<Estudiantes> repositorio = new RepositorioBase<Estudiantes>();
                if (repositorio.Eliminar(id))
                {

                    Utilitarios.Utils.ShowToastr(this.Page, "Eliminado con exito!!", "Eliminado", "info");
                }
                else
                    Utilitarios.Utils.ShowToastr(this.Page, "Fallo al Eliminar :(", "Error", "error");
                Limpiar();
            }
            else
            {
                Utilitarios.Utils.ShowToastr(this.Page, "No se pudo eliminar un estudiante que no existe", "error", "error");

            }

        }
    }

}