using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RepositorioEvaluacion : RepositorioBase<Evaluaciones>
    {
        public override Evaluaciones Buscar(int id)
        {
            Evaluaciones Evaluaciones = new Evaluaciones();
            Contexto db = new Contexto();
            try
            {

                Evaluaciones = db.evaluaciones.Include(x => x.Detalle)
                    .Where(x => x.EvaluacionId == id).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }

            return Evaluaciones;
        }

        public override bool Modificar(Evaluaciones entity)
        {
            bool paso = false;
            Contexto db = new Contexto();
            RepositorioBase<Estudiantes> dbE = new RepositorioBase<Estudiantes>();


            try
            {
                var anterior = new RepositorioBase<Evaluaciones>().Buscar(entity.EvaluacionId);
                var estudiante = dbE.Buscar(entity.EstudianteId);

                estudiante.PuntosPerdidos = entity.TotalPerdido;

                foreach (var item in anterior.Detalle)
                {
                    if (!entity.Detalle.Any(A => A.DetalleId == item.DetalleId))
                    {
                        db.Entry(item).State = EntityState.Deleted;

                    }

                }

                foreach (var item in entity.Detalle)
                {
                    if (item.DetalleId == 0)
                    {

                        db.Entry(item).State = EntityState.Added;
                    }

                    else
                        db.Entry(item).State = EntityState.Modified;
                }



                dbE.Modificar(estudiante);

                db.Entry(entity).State = EntityState.Modified;

                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }


            return paso;
        }


        public override bool Guardar(Evaluaciones entity)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                RepositorioBase<Estudiantes> Est = new RepositorioBase<Estudiantes>();

                if (db.evaluaciones.Add(entity) != null)
                {
                    var estudiante = Est.Buscar(entity.EstudianteId);


                    estudiante.PuntosPerdidos += entity.TotalPerdido;
                    paso = db.SaveChanges() > 0;
                    Est.Modificar(estudiante);
                }

            }
            catch (Exception)
            {
                throw;
            }

            return paso;
        }
    }
}
 