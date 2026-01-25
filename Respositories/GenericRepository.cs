using System.Linq.Expressions;
using InMemoryDBSpecificationRepositoryUOWProject.Context;
using InMemoryDBSpecificationRepositoryUOWProject.Models;
using InMemoryDBSpecificationRepositoryUOWProject.Specifications;
using Microsoft.EntityFrameworkCore;

namespace InMemoryDBSpecificationRepositoryUOWProject.Respositories
{
    //public interface IGenericReadRepository<TEntity> where TEntity : BaseEntity
    //{
    //    List<TEntity> GetAll(ISpecification<TEntity>? spec = null);
    //    TEntity GetById(ISpecification<TEntity>? spec = null);
    //}
    //public interface IGenericWriteRepository<TEntity> where TEntity : BaseEntity
    //{
    //    void Insert(TEntity entity);
    //    void InsertRange(IEnumerable<TEntity> entities);
    //    void Delete(TEntity entity);
    //    void DeleteRange(IEnumerable<TEntity> entities);
    //    void Attach(TEntity entity);
    //}
    //public class GenericReadRepository<TEntity> : IGenericReadRepository<TEntity> where TEntity : BaseEntity
    //{
    //    protected readonly InMemoryDBContext _context;

    //    public GenericReadRepository(InMemoryDBContext context)
    //    {
    //        _context = context;
    //    }

    //    public virtual List<TEntity> GetAll(ISpecification<TEntity>? spec = null)
    //    {
    //        IQueryable<TEntity> query = _context.Set<TEntity>();
    //        if(spec != null)
    //            query = query.ApplySpecification(spec);
    //        //query = query.AsNoTracking();
    //        return query.ToList();
    //    }

    //    public virtual TEntity GetById(ISpecification<TEntity>? spec = null)
    //    {
    //        IQueryable<TEntity> query = _context.Set<TEntity>();
    //        if (spec != null)
    //            query = query.ApplySpecification(spec);
    //        //query = query.AsNoTracking();
    //        return query.FirstOrDefault();
    //    }
    //}

    //public class GenericWriteRepository<TEntity> : IGenericWriteRepository<TEntity> where TEntity : BaseEntity
    //{
    //    protected readonly InMemoryDBContext _context;

    //    public GenericWriteRepository(InMemoryDBContext context)
    //    {
    //        _context = context;
    //    }

    //    public void Insert(TEntity entity)
    //    {
    //        _context.Set<TEntity>().Add(entity);
    //    }
    //    public void InsertRange(IEnumerable<TEntity> entities)
    //    {
    //        _context.Set<TEntity>().AddRange(entities);
    //    }
    //    public void Delete(TEntity entity)
    //    {
    //        _context.Set<TEntity>().Remove(entity);
    //    }
    //    public void DeleteRange(IEnumerable<TEntity> entities)
    //    {
    //        _context.Set<TEntity>().RemoveRange(entities);
    //    }
    //    public void Attach(TEntity entity)
    //    {
    //        _context.Set<TEntity>().Attach(entity);
    //    }
    //}
}
