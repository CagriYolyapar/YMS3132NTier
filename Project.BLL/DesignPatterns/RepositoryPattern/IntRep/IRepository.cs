using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.RepositoryPattern.IntRep
{
    public interface IRepository<T> where T : BaseEntity
    {
        //Listeleme Metotlari

        List<T> GetAll();

        List<T> GetPassives();

        List<T> GetActives();

        List<T> GetModifieds();

        //Modifikasyonlar

        void Add(T item);

        void Delete(T item);

        void Update(T item);

        void Destroy(T item);


        //Queries

        List<T> Where(Expression<Func<T, bool>> exp);

        T FirstOrDefault(Expression<Func<T, bool>> exp);

        bool Any(Expression<Func<T, bool>> exp);

        object Select(Expression<Func<T, object>> exp);

        T Find(int id);


    }
}
