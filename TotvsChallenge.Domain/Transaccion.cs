using System;
using System.Collections.Generic;
using System.Text;

namespace TotvsChallenge.Domain
{
    public class Transaccion
    {
        public int Id { get; set; }
        public int MontoPagado { get; set; }
        public int MontoDevuelto { get; set; }
    }
}
