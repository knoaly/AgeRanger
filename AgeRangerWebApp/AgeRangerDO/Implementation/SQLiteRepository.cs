using AgeRangerDO.Interface;
using SQLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerDO.Implementation
{
    public class SQLiteRepository<TEntity> : IRepository<TEntity> where TEntity : new()
    {
        private SQLiteConnection _context;
        protected SQLiteConnection Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public SQLiteRepository()
        {
            string path = ConfigurationManager.AppSettings["SQLitePath"];
            Context = new SQLiteConnection(path);
        }

        public SQLiteRepository(SQLiteConnection context)
        {
            _context = context;
        }

        public virtual long Insert(TEntity model)
        {
            long iRes = Context.Insert(model);
            return iRes;
        }

        public long Update(TEntity model)
        {
            long iRes = Context.Update(model);
            return iRes;
        }

        public long Delete(TEntity model)
        {
            long iRes = Context.Delete(model);
            return iRes;
        }

        public TEntity Select(long pk)
        {
            var map = Context.GetMapping(typeof(TEntity));
            return Context.Query<TEntity>(map.GetByPrimaryKeySql, pk).First();
        }

        public IEnumerable<TEntity> SelectAll()
        {
            return Context.Table<TEntity>();
        }

        public long GetLastInsertId()
        {
            long key = Context.ExecuteScalar<long>("SELECT last_insert_rowid()");
            return key;
        }
    }
}
