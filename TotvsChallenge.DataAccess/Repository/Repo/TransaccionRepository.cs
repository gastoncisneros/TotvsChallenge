using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using TotvsChallenge.DataAccess.Repository.Interface;
using TotvsChallenge.Domain;

namespace TotvsChallenge.DataAccess.Repository.Repo
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransaccionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void GuardarTransaccion(Transaccion transaccion)
        {
            try
            {
                _context.Transacciones.Add(transaccion);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
