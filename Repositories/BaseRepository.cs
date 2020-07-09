using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProductApi.Repositories
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
        public void Update(Object updateObject)
        {
            _set.Attach((T)updateObject);
            _context.Entry(updateObject).State = EntityState.Modified;

            _context.Update(updateObject);

        }
        public void Delete(Guid Id)
        {
            T toDelete = _set.Find(Id);
            _context.Remove(toDelete);
        }

        public void Create(Object o)
        {
            _context.Add(o);
        }
    }
}
