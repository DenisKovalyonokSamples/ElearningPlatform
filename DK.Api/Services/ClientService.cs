using DK.Dal.Entities;
using DK.Dal.Interfaces;
using DK.Dal.Repositories;
using DK.Dal.Transactions;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DK.Api.Services
{
    public class ClientService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IRepository<Client> _repository;

        public ClientService()
        {
            _unitOfWork = new UnitOfWork(WebApiApplication.SessionFactory);
            _repository = new CoreRepository<Client>(WebApiApplication.GetCurrentSession());
        }

        public int Add(Client entity)
        {
            int result = 0;

            entity.Id = 0;
            entity.CreatedOn = DateTime.UtcNow;

            try
            {
                result = _repository.Add(entity);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);

                _unitOfWork.Rollback();
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            return result;
        }

        public bool Update(Client entity)
        {
            bool result = true;

            try
            {
                _repository.Update(entity);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);

                _unitOfWork.Rollback();
                result = false;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            return result;
        }

        public bool Delete(int id)
        {
            bool result = true;

            try
            {
                var entity = _repository.FindBy(id);

                if (entity != null)
                {
                    _repository.Delete(entity);
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);

                _unitOfWork.Rollback();
                result = false;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            return result;
        }

        public Client Get(int id)
        {
            Client result = new Client();

            try
            {
                result = _repository.FindBy(id);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return result;
        }

        public Client FindBy(Expression<Func<Client, bool>> expression)
        {
            Client result = new Client();

            try
            {
                result = _repository.FindBy(expression);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return result;
        }

        public List<Client> GetAll()
        {
            List<Client> result = new List<Client>();

            try
            {
                result = _repository.Items?.ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return result;
        }

        public List<Client> FilterBy(Expression<Func<Client, bool>> expression)
        {
            List<Client> result = new List<Client>();

            try
            {
                result = _repository.FilterBy(expression)?.ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}