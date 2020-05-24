using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TotvsChallenge.DataAccess.Repository.Interface;
using TotvsChallenge.Domain;

namespace TotvsChallenge.DataAccess.Repository.Repo
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public TransaccionRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Se utiliza DAPPER ORM para guardar las transacciones,
        /// pero tambien se puede usar Entity Framework con _context
        /// </summary>
        /// <param name="transaccion">Transaccion a guardar</param>
        public void GuardarTransaccion(Transaccion transaccion)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    string insertQuery = "INSERT INTO Transacciones (MontoPagado, MontoDevuelto) Values (@MontoPagado, @MontoDevuelto)";

                    var result = db.Execute(insertQuery, new
                    {
                        transaccion.MontoPagado,
                        transaccion.MontoDevuelto
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
