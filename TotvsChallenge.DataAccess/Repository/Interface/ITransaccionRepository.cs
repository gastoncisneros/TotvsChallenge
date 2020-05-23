using System;
using System.Collections.Generic;
using System.Text;
using TotvsChallenge.Domain;

namespace TotvsChallenge.DataAccess.Repository.Interface
{
    public interface ITransaccionRepository
    {
        void GuardarTransaccion(Transaccion transaccion);
    }
}
