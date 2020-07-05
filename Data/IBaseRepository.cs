﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ProductApi.Models;
using System;
using System.Collections.Generic;

namespace ProductApi.Data
{
    public interface IBaseRepository<T> where T : class
    {
        void Create(Object o);
        void Delete(Object o);
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        IEnumerable<T> GetByName(string search);
        void Update(Object o);
    }
}