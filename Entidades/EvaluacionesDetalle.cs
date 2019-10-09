﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class EvaluacionesDetalle
    {
        [Key]
        public int DetalleId { get; set; }
        public int EvaluacionId { get; set; }
        public string Categorias { get; set; }
        public decimal Valor { get; set; }
        public decimal Logrado { get; set; }
        public decimal Perdido { get; set; }

        public EvaluacionesDetalle()
        {
            DetalleId = 0;
            EvaluacionId = 0;
            Categorias = string.Empty;
            Valor = 0;
            Logrado = 0;
            Perdido = 0;
        }

        public EvaluacionesDetalle(int detalleID, int EvaluacionID, string categorias, decimal valor, decimal logrado, decimal perdido)
        {
            DetalleId = detalleID;
            EvaluacionId = EvaluacionID;
            Categorias = categorias;
            Valor = valor;
            Logrado = logrado;
            Perdido = perdido;
        }
    }
}
