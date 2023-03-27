using Microsoft.EntityFrameworkCore;
using RestASPNET.Model;
using RestASPNET.Model.Base;
using RestASPNET.Model.Context;
using System;
using System.Data;

namespace RestASPNET.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected MySqlContext _context;
        private DbSet<T> _dataset;

        public GenericRepository(MySqlContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }
        public T FindById(int id)
        {
            return _dataset.SingleOrDefault(i => i.Id.Equals(id));
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dataset.ToListAsync();
        }
        public T Create(T item)
        {
            try
            {
                _dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public T Update(T item)
        {
            if (!Exists(item.Id)) return null;

            var result = _dataset.FirstOrDefault(i => i.Id.Equals(item.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        public void Delete(int id)
        {
            var result = _dataset.FirstOrDefault(i => i.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exists(int id)
        {
            return _dataset.Any(p => p.Id.Equals(id));
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return _dataset.FromSqlRaw<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            var result = "";
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command =  connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }
            return int.Parse(result);
        }
    }
}
