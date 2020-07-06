using Dapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using ProductApi.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace ProductApi.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {

        private readonly ApplicationContext _context;

        private readonly DbSet<T> _set;
        protected virtual string TableName => typeof(T).Name;
        protected virtual string Identifier => "Id";

        public BaseRepository(ApplicationContext context, DbSet<T> set)
        {
            _context = context;
            _set = set;
            
        }
        public IEnumerable<T> GetAll()
        {

            return _set.ToArray();
        }
        public T GetById(Guid id)
        {
            return _set.Find(id);
        }

        public IEnumerable<T> GetByName(string search)
        {
            var searchResult = _set.Where(o => o.Name.Contains(search)).ToArray();
            return searchResult;
        }
        public void Update(Object o)
        {
            _context.Update(o);
            _context.SaveChanges();

        }
        public void Delete(Object o)
        {
            _context.Remove(o);
            _context.SaveChanges();
        }

        public void Create(Object o)
        {
            _context.Add(o);
            _context.SaveChanges();
        }
    }
}
