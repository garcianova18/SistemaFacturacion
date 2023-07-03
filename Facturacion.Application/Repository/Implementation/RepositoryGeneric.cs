using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Facturacion.Application.Repository.Interfaces;
using Facturacion.Domain.Models;
using Facturacion.Infrastruture.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Application.Repository.Implementation
{
    public class RepositoryGeneric<TEntity> : IRepositoryGeneric<TEntity> where TEntity : class
    {
        private readonly SistemaFacturacionContext _context;
        private DbSet<TEntity> _entitites;

        public RepositoryGeneric(SistemaFacturacionContext context)
        {
            _context = context;
            _entitites = _context.Set<TEntity>();
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            try
            {
                await _entitites.AddAsync(entity);


            }
            catch (Exception)
            {

                throw;
            }

            return entity;
        }

        public void Delete(TEntity entity)
        {
            try
            {
                _entitites.Remove(entity);


            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> filters = null)
        {

            if (filters is null)
            {
                return false;
            }

            return await _entitites.AnyAsync(filters);
        }



        public async Task<List<TEntity>> GetAll()
        {


            return await _entitites.ToListAsync();

        }

        public async Task<TEntity> GetByid(int id)
        {
            return await _entitites.FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;



            }
            catch (Exception)
            {

                throw;
            }


        }


        public async Task<int> ExistsUpdate(Expression<Func<TEntity, bool>> filters = null)
        {
            if (filters is null)
            {
                return 0;
            }

            return await _entitites.Where(filters).CountAsync();


        }

      



    }
}
