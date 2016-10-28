using FactoryShahin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShahinShoese
{
    public static class ClassLink
    {
        static ShahinFactoryDb db = new ShahinFactoryDb();
        public static void FillComboBox<T>(System.Windows.Forms.ComboBox C) where T : class,new()
        {
            T E=new T();
            C.DataSource=db.Set<T>().ToList();
            C.DisplayMember = E.GetType().GetProperties().FirstOrDefault(x => x.Name.Contains(E.GetType().Name + "Name")).Name;
            C.ValueMember = E.GetType().GetProperties().FirstOrDefault(x => x.Name.Contains(E.GetType().Name + "ID")).Name;
        }
        public static List<T> GetEntity<T>() where T : class
        {
            return db.Set<T>().ToList();
        }
        public static void Edit()
        {
            db.SaveChanges();
        }
        public static void Register(Object Ob)
        {
            db.Entry(Ob).State = EntityState.Added;
            db.SaveChanges();
        }
        public static void Remove(Object Ob)
        {
            db.Entry(Ob).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }
}
