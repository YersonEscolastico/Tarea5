using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Evaluaciones
    {
        [Key]

        public int EvaluacionId { get; set; }
        public int EstudianteId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalPerdido { get; set; }
        public List<EvaluacionesDetalle> Detalle { get; set; }


        public Evaluaciones(int evaluacionid, DateTime fecha, int estudianteid, decimal totalPerdido)
        {
            EvaluacionId = evaluacionid;
            Fecha = fecha;
            EstudianteId = estudianteid;
            TotalPerdido = totalPerdido;
            Detalle = new List<EvaluacionesDetalle>();
        }
        public Evaluaciones()
        {
            EvaluacionId = 0;
            EstudianteId = 0;
            Fecha = DateTime.Now;
            TotalPerdido = 0;
            Detalle = new List<EvaluacionesDetalle>();
        }
        public void AgregarDetalle(int detalleID, int EvaluacionID, string categorias, decimal valor, decimal logrado, decimal perdido)
        {
            this.Detalle.Add(new EvaluacionesDetalle(detalleID, EvaluacionID, categorias, valor, logrado, perdido));
        }
    }
}
