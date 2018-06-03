using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Gallery.Interface;
using System.Diagnostics;

namespace CustomGallery
{
	public class DBHelper
	{
		private static readonly object _padlock = new object();
		private static readonly object _insertPadlock = new object();
		private  SQLiteConnection _sqlconnection;

		private static DBHelper _appDb = null; 

		private DBHelper(string dbName)
		{
			//Getting conection and Creating table
			_sqlconnection = DependencyService.Get<ILocalStorage>().GetConnection(dbName);
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <returns>The instance.</returns>
		/// <param name="dbName">Db name.</param>
		public static DBHelper GetInstance(string dbName)
		{

			lock (_padlock)
			{
                if (_appDb == null)
                {
                    _appDb = new DBHelper(dbName);
                    return _appDb;
                }
                return _appDb;
			}
		}

		/// <summary>
		/// Resets the instance.
		/// </summary>
		/// <param name="dbName">Db name.</param>
		public static void ResetInstance(string dbName)
		{ 
					_appDb = null;  
		}

		/// <summary>
		/// Gets the SQL ite conncetion instance.
		/// </summary>
		/// <returns>The SQL ite conncetion instance.</returns>
		public SQLiteConnection GetSQLiteConncetionInstance()
		{
			return _sqlconnection;
		}
        /// <summary>
        /// method for rollback a transaction if transaction is not succeed
        /// </summary>
        public void RollBackPendingChanges()
        {
            _sqlconnection.Rollback();
        }
        /// <summary>
        /// Method to fetch last record out of 30 recent record from SyncStatusDetail table according to date time
        /// </summary>
        /// <returns></returns>
      
        /// <summary>
        /// method for finishing point of an transaction
        /// </summary>
        public void CommitTranscation()
        {
            _sqlconnection.Commit();
        }
        /// <summary>
        /// method for starting point of an transaction.
        /// </summary>
        public void BeginTrancaction()
        {
            _sqlconnection.BeginTransaction();
        }

		/// <summary>
		/// Creates the table.
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void CreateTable<T>()
		{
			try
			{
				_sqlconnection.CreateTable<T>();
			}
			catch (Exception e)
			{
				Debug.WriteLine("Exception on Creating Table in Sqlite: {0}", e.Message);
			}
		}

		/// <summary>
		/// Add new Object to DB.
		/// </summary>
		/// <param name="obj">Object.</param>
		public void InsertObject(Object obj)
		{
			lock(_insertPadlock)
			{
				try
				{
					_sqlconnection.Insert(obj);
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine("Exception on Insert data into Sqlite: {0}", e.Message);
				}
			}
		}


		/// <summary>
		/// Add new Objects collection to DB
		/// </summary>
		/// <param name="objs">Objects.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void InsertAllObject<T>(IEnumerable<T> objs) where T : class
		{
			lock (_insertPadlock)
			{
				try
				{
					_sqlconnection.InsertAll(objs);
				}
				catch (Exception e)
				{
					Debug.WriteLine("Exception on Insert data into Sqlite: {0}", e.Message);
				}
			}
		}

		/// <summary>
		/// Add new Object to DB.
		/// </summary>
		/// <param name="obj">Object.</param>
		public void DeleteObject(Object obj)
		{
			lock(_insertPadlock)
			{
				try
				{
					_sqlconnection.Delete(obj);
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine("Exception on Deleteing Object in Sqlite: {0}", e.Message);
				}
			}
		}

		/// <summary>
		/// Get all Objects.
		/// </summary>
		/// <returns>The all objects.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public IEnumerable<T> GetAllObjects<T>() where T : class
		{
			lock(_insertPadlock)
			{
				/* return (from t in _sqlconnection.Table<User>() select t).ToList();*/
				IEnumerable<T> objs;
				try
				{
					objs = (from t in _sqlconnection.Table<T>() select t).ToList();
				}
				catch
				{
					objs = null;
				}
				return objs;
			}
		}

		/// <summary>
		/// Delete specific object.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void DeleteObject<T>(int id)
		{
			lock(_insertPadlock)
			{
				try
				{
					_sqlconnection.Delete<T>(id);
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine("Exception on Delete object in Sqlite: {0}", e.Message);
				}
				System.Diagnostics.Debug.WriteLine("deleted record id : " + id);
			}
		}

		/// <summary>
		/// Delete specific object.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void DeleteObject<T>(long id)
		{
			lock(_insertPadlock)
			{
				try
				{
					_sqlconnection.Delete<T>(id);
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine("Exception on Delete Specific object in Sqlite: {0}", e.Message);
				}
				System.Diagnostics.Debug.WriteLine("deleted record id : " + id);
			}
		}

		/// <summary>
		/// Deletes the objects.
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void DeleteObjects<T>()
		{
			lock(_insertPadlock)
			{
				try
				{
					_sqlconnection.DeleteAll<T>();
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine("Exception on Update Sqlite: {0}", e.Message);
			}
			}
		}

		/// <summary>
		/// Updates the object.
		/// </summary>
		/// <param name="obj">Object.</param>
		public void UpdateObject(Object obj)
		{
			lock(_insertPadlock)
			{
				try
				{
					_sqlconnection.Update(obj);
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine("Exception on Update Sqlite: {0}", e.Message);
			}
			}
		}

		/// <summary>
		/// Updates all objects.
		/// </summary>
		/// <param name="enumObjs">Enum objects.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void UpdateAllObjects<T>(IEnumerable<T> enumObjs)
		{
			lock (_insertPadlock)
			{
				try
				{
					_sqlconnection.UpdateAll(enumObjs);

				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine("Exception on Updating All Objects in Sqlite: {0}", e.Message);
				}
			}
		}

		/// <summary>
		/// Get number of entries in DB.
		/// </summary>
		/// <returns>The objects count.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public int GetObjectsCount<T>() where T : class
		{
			lock (_insertPadlock)
			{
				try
				{
					IEnumerable<T> objs = GetAllObjects<T>();
					return objs != null ? objs.Count<T>() : 0;
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine("Exception on get Object Count in Sqlite: {0}", e.Message);
					return 0;
				}
			}
		}
		/// <summary>
		/// Queries the db.
		/// </summary>
		/// <returns>The db.</returns>
		/// <param name="query">Query.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public IEnumerable<T> QueryDB<T>(string query) where T : class
		{
			lock (_insertPadlock)
			{
				System.Diagnostics.Debug.WriteLine("In QueryDB<T>");
				try
				{
					//_sqlconnection.Query<User>("SELECT * FROM [User] WHERE [userId] = 1");
					return _sqlconnection.Query<T>(query);
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine("Exception on Getting Query from Sqlite: {0}", e.Message);
					return null;
				}
			}
		}
	}
}
