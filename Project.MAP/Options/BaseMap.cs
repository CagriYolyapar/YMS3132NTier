using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public abstract class BaseMap<T>:EntityTypeConfiguration<T> where T : BaseEntity
    {
        //İstendigi takdirde veritabanı custom ayarları ortak olan özellikler icin burada yapılabilir
    }
}
